﻿using Nethereum.Hex.HexTypes;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using SmarTreaty.Common.Core.Helpers.Interfaces;
using SmarTreaty.Common.Core.Services.Interfaces;
using SmarTreaty.Common.DomainModel;
using System;
using System.Threading.Tasks;

namespace SmarTreaty.Business.Services
{
    public class SmartContractService : BaseService, ISmartContractService
    {
        private string _privateKey;
        private string _endndpointUrl = "http://testchain.nethereum.com:8545";  // TODO move it to config

        public SmartContractService(IDatabaseWorkUnit db) : base(db)
        {
            // TODO get private key from db
            _privateKey = "0xb5b1870957d373ef0eeffecc6e4812c0fd08f554b37b233526acc331bf1544f7";
        }


        public async Task DeployContract(SmartContract contract, params object[] values)
        {
            var account = new Account(_privateKey);
            var web3 = new Web3(account, _endndpointUrl);

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