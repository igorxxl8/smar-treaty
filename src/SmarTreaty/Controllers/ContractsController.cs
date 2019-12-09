using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SmarTreaty.Common.Core.Services.Interfaces;
using SmarTreaty.Common.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SmarTreaty.Controllers
{
    [RoutePrefix("contracts")]
    public class ContractsController : DefaultController
    {
        private class ContractFunction
        {
            public class ContractInput
            {
                public string name { get; set; }
                public string type { get; set; }
            }

            List<ContractInput> inputs { get; set; }
        }

        private readonly ISmartContractService _smartContractService;

        public ContractsController(ISmartContractService smartContractService)
        {
            _smartContractService = smartContractService;
        }

        // GET: Contracts
        public ActionResult Index()
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

        [Route("templates")]
        public ActionResult Templates()
        {
            try
            {
                return View(new List<SmartContract>()); // need 'GetAllTemplates' method
            }
            catch
            {
                return HttpNotFound();
            }
        }

        public ActionResult Create(int? templateId)
        {
            try
            {
                // call getTemplate API
                string resultJson = "[{\"type\":{\"value\":\"function\"},\"name\":\"multiply\",\"inputs\":[{\"name\":\"val\",\"type\":\"int256\",\"components\":null,\"indexed\":null}],\"outputs\":[{\"name\":\"result\",\"type\":\"int256\",\"components\":null}],\"payable\":false,\"stateMutability\":{\"value\":\"nonpayable\"},\"constant\":false,\"anonymous\":null},{\"type\":{\"value\":\"function\"},\"name\":\"test_\",\"inputs\":[{\"name\":\"multiplier\",\"type\":\"int256\",\"components\":null,\"indexed\":null}],\"outputs\":[],\"payable\":false,\"stateMutability\":{\"value\":\"nonpayable\"},\"constant\":false,\"anonymous\":null},{\"type\":{\"value\":\"constructor\"},\"name\":null,\"inputs\":[],\"outputs\":null,\"payable\":false,\"stateMutability\":{\"value\":\"nonpayable\"},\"constant\":null,\"anonymous\":null}]";
                var parsed = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(resultJson);
                var parsedSOmething = JsonConvert.DeserializeObject(resultJson);

                List<Dictionary<string, string>> fields = new List<Dictionary<string, string>>();
                foreach (var tokens in parsed.Select(x => ((JArray)x["inputs"]).Select(y => y.Children())))
                {
                    if (tokens.Count() > 0)
                    {
                        List<JProperty> properties = tokens.First().Select(x => (JProperty)x).ToList();
                        fields.Add(new Dictionary<string, string>());
                        foreach (var property in properties.Take(2))
                            fields.Last().Add(property.Name, ((JValue)property.Value).Value.ToString());
                    }
                }

                return View(fields);
            }
            catch
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        public ActionResult Create(List<Dictionary<string, string>> contractData)
        {
            try
            {
                // use fields to create contract

                return RedirectToAction("Index");
            }
            catch
            {
                return HttpNotFound();
            }
        }
    }
}