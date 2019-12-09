using Newtonsoft.Json;
using SmarTreaty.Common.Core.Services.Interfaces;
using SmarTreaty.Common.DomainModel;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SmarTreaty.Controllers
{
    [RoutePrefix("contracts")]
    public class ContractsController : DefaultController
    {
        private readonly ISmartContractService _smartContractService;

        public ContractsController(ISmartContractService smartContractService)
        {
            _smartContractService = smartContractService;
        }

        // GET: Contracts
        public async Task<ActionResult> Index()
        {
            var res = await _smartContractService.CompileContract(@"pragma solidity >=0.4.22 <0.6.0;contract test_xtemplate {int _multiplier;constructor(int init) public {}function test_(int multiplier) public {_multiplier = multiplier;}function multiply(int val) public returns(int result) {result = val * _multiplier;}}");
            var k = JsonConvert.DeserializeObject<object[]>(res);
            var template = new SmartContract
            {
                Abi = JsonConvert.SerializeObject(k[0]),
                ByteCode = (string)k[1]
            };
            var a = new object[] { 1 };

            try
            {
                await _smartContractService.DeployContract(template, "0xb5b1870957d373ef0eeffecc6e4812c0fd08f554b37b233526acc331bf1544f7");
            }
            catch
            {

            }
            return View();
        }
    }
}