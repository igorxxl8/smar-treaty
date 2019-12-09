using SmarTreaty.Common.Core.Services.Interfaces;
using SmarTreaty.Common.DomainModel;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

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

        private readonly ITemplateService _templateService;

        public ContractsController(ITemplateService templateService)
        {
            _templateService = templateService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Templates()
        {
            try
            {
                return View(new List<Template>()); // need 'GetAllTemplates' method
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