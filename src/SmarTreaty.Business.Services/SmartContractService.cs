using Nethereum.Hex.HexTypes;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using SmarTreaty.Common.Core.Helpers.Interfaces;
using SmarTreaty.Common.Core.Services.Interfaces;
using SmarTreaty.Common.DomainModel;
using System;
using System.Configuration;
using System.Net.Http;
using System.Numerics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SmarTreaty.Business.Services
{
    public class SmartContractService : BaseService, ISmartContractService
    {
        private readonly string _endpointUrl = ConfigurationManager.AppSettings["ChainEndpoint"];
        private readonly string _compilerUrl = ConfigurationManager.AppSettings["CompilerApi"];
        private static readonly HttpClient client = new HttpClient();

        public SmartContractService(IDatabaseWorkUnit db) : base(db)
        {

        }

        public async Task<string> CompileContract(string source)
        {
            source = Regex.Replace(source, @"[\r\n\t]", "");
            HttpResponseMessage response = await client.GetAsync($"{_compilerUrl}?source={source}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            return null;
        }

        public async Task DeployContract(SmartContract contract, User user, string contractName, params object[] values)
        {
            var account = new Account(user.PrivateKey);
            var web3 = new Web3(account, _endpointUrl);
            var totalSupply = BigInteger.Parse("1000000000000000000");

            contract.Abi = contract.Abi
                .Replace("{\"value\":\"function\"}", "\"function\"")
                .Replace(",\"components\":null,\"indexed\":null", "")
                .Replace(",\"components\":null", "")
                .Replace("\"type\":{\"value\":\"constructor\"}", "\"type\": \"constructor\"")
                .Replace("\"stateMutability\":{\"value\":\"nonpayable\"}", "\"stateMutability\": \"nonpayable\"")
                .Replace("\"name\": null,", "")
                .Replace("\"outputs\": null,", "")
                .Replace(",\"constant\": null,", "")
                .Replace("\"anonymous\": null", "");
            var estimatedGas = await EstimateGas(web3, contract, account.Address, totalSupply);
            var contractAddress = await TryDeployContract(web3, contract, account.Address, estimatedGas, values);

            Db.Contracts.Add(new Contract
            {
                Id = Guid.NewGuid(),
                Address = contractAddress,
                CreationDate = DateTime.Now,
                Name = contractName,
                User = user
            });
            Db.Save();
        }

        private Task<HexBigInteger> EstimateGas(Web3 web3, SmartContract contract, string senderAddress, BigInteger totalSupply)
        {
            try
            {
                return web3.Eth.DeployContract.EstimateGasAsync(contract.Abi, contract.ByteCode, senderAddress, totalSupply);
            }
            catch
            {
                throw new Exception("Cannot estimate gas");
            }
        }

        private async Task<string> TryDeployContract(
            Web3 web3,
            SmartContract contract,
            string senderAddress,
            HexBigInteger estimatedGas,
            params object[] values
        )
        {
            try
            {
                var receipt = await web3.Eth.DeployContract.SendRequestAndWaitForReceiptAsync(
                    contract.Abi,
                    contract.ByteCode,
                    senderAddress,
                    estimatedGas,
                    null,
                    values
                );

                return receipt.ContractAddress;
            }
            catch
            {
                throw new Exception("Cannot deploty contract");
            }
        }

        public void SaveTemplate(SmartContract smartContract)
        {
            Db.SmartContracts.Add(smartContract);
            Db.Save();
        }

        public SmartContract GetTemplate(Guid id)
        {
            return Db.SmartContracts.GetById(id);
        }
    }
}
