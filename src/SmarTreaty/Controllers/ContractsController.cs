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
        private readonly IUserService _userService;

        public ContractsController(ISmartContractService smartContractService, IUserService userService)
        {
            _smartContractService = smartContractService;
            _userService = userService;
        }

        // GET: Contracts
        [Route("")]
        public ActionResult Index(DateTime? startDate, DateTime? endDate)
        {
            var user = _userService.GetUsers(u => u.FirstName + " " + u.LastName == HttpContext.User.Identity.Name).FirstOrDefault();
            var list = user.Contracts;

            if (endDate != null)
            {
                list = list.Where(c => c.CreationDate <= endDate).ToList();
            }

            if (startDate != null)
            {
                list = list.Where(c => c.CreationDate >= startDate).ToList();
            }

            return View(list);
        }

        [Route("templates")]
        public ActionResult Templates(string search)
        {
            var templates = _userService.GetUsers(properties: "SmartContracts").SelectMany(u => u.SmartContracts);
            if (!string.IsNullOrEmpty(search))
            {
                templates = templates.Where(t => t.Name.Contains(search)).ToList();
            }

            return View(templates.ToList());
        }

        public ActionResult Create(Guid templateId)
        {
            var template = _smartContractService.GetTemplate(templateId);
            ViewBag.TemplateName = template.Name.Trim();
            ViewBag.TemplateId = template.Id;

            string resultJson = template.Abi;
            var parsed = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(resultJson);

            List<Dictionary<string, string>> fields = new List<Dictionary<string, string>>();

            foreach (var tokens in parsed.Select(x => ((JArray)x["inputs"]).Select(y => y.Children())))
            {
                if (tokens.Count() > 0)
                {
                    List<JProperty> properties = tokens.First().Select(x => (JProperty)x).ToList();
                    fields.Add(new Dictionary<string, string>());

                    foreach (var property in properties.Take(2))
                    {
                        fields.Last().Add(property.Name, ((JValue)property.Value).Value.ToString());

                    }
                }
            }

            return View(fields);
        }

        [HttpPost]
        public async Task<ActionResult> Create(List<Dictionary<string, string>> contractData)
        {
            var templateId = contractData[0]["value"];

            var contractName = contractData[1]["value"];
            //var values = new object[contractData.Count - 2];
            var values = 1;
            //for (int i = 2; i < contractData.Count; i++)
            //{
            //    values[i - 2] = contractData[i]["value"];
            //}

            var template = _smartContractService.GetTemplate(Guid.Parse(templateId));
            var user = _userService.GetUsers(u => u.FirstName + " " + u.LastName == HttpContext.User.Identity.Name).FirstOrDefault();

            try
            {
                await _smartContractService.DeployContract(template, user, contractName, values);
            }
            catch (Exception ex)
            {

            }

            return RedirectToAction("Index", "Contracts");
        }
    }
}