using Nethereum.Hex.HexTypes;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using SmarTreaty.Common.Core.Helpers.Interfaces;
using SmarTreaty.Common.Core.Services.Interfaces;
using SmarTreaty.Common.DomainModel;
using System;
using System.Configuration;
using System.Net.Http;
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
            HttpResponseMessage response = await client.GetAsync($"{_compilerUrl}?source={source}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            return null;
        }

        public async Task DeployContract(SmartContract contract, string privateKey, params object[] values)
        {
            var account = new Account(privateKey);
            var web3 = new Web3(account, _endpointUrl);

            var estimatedGas = await EstimateGas(web3, contract, account.Address);
            var contractAddress = await TryDeployContract(web3, contract, account.Address, estimatedGas, values);

            // TODO save contractAddress to db
        }

        private Task<HexBigInteger> EstimateGas(Web3 web3, SmartContract contract, string senderAddress)
        {
            try
            {
                return web3.Eth.DeployContract.EstimateGasAsync(contract.Abi, contract.ByteCode, senderAddress, new HexBigInteger(3000000));
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
    }
}
