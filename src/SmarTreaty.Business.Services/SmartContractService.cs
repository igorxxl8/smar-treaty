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
        private readonly string _endndpointUrl = ConfigurationManager.AppSettings["TestChain"];
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

        public async Task DeployContract(Template template, string privateKey, params object[] values)
        {
            //_privateKey = "0xb5b1870957d373ef0eeffecc6e4812c0fd08f554b37b233526acc331bf1544f7";
            var account = new Account(privateKey);
            var web3 = new Web3(account, _endndpointUrl);

            var estimatedGas = await EstimateGas(web3, template, account.Address);
            var contractAddress = await TryDeployContract(web3, template, account.Address, estimatedGas, values);

            var contract = new Contract
            {
                Address = contractAddress
            };


            Db.Contracts.Add(contract);
        }

        private Task<HexBigInteger> EstimateGas(Web3 web3, Template template, string senderAddress)
        {
            try
            {
                return web3.Eth.DeployContract.EstimateGasAsync(template.Abi, template.ByteCode, senderAddress, new HexBigInteger(3000000));
            }
            catch
            {
                throw new Exception("Cannot estimate gas");
            }
        }

        private async Task<string> TryDeployContract(
            Web3 web3,
            Template template,
            string senderAddress,
            HexBigInteger estimatedGas,
            params object[] values
        )
        {
            try
            {
                var receipt = await web3.Eth.DeployContract.SendRequestAndWaitForReceiptAsync(
                    template.Abi,
                    template.ByteCode,
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
