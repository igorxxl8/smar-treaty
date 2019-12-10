﻿using Newtonsoft.Json;
using SmarTreaty.Common.Core.Services.Interfaces;
using SmarTreaty.Common.DomainModel;
using SmarTreaty.Common.ViewModels.Templates;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SmarTreaty.Areas.editor.Controllers
{
    [RoutePrefix("templates")]
    [Route("{action}/{id}")]
    public class TemplatesController : EditorController
    {
        private readonly ISmartContractService _smartContractService;
        private readonly IUserService _userService;

        public TemplatesController(ISmartContractService smartContractService, IUserService userService)
        {
            _smartContractService = smartContractService;
            _userService = userService;
        }

        [Route("")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("create")]
        [HttpGet]
        public ActionResult Create()
        {
            var model = new CreateTemplateViewModel();

            return View(model);
        }

        [Route("create")]
        [HttpPost]
        public async Task<ActionResult> Create(CreateTemplateViewModel model)
        {
            if (!model.Verified)
            {
                var compiledContractString = await _smartContractService.CompileContract(model.Source);
                if (compiledContractString == null)
                {
                    return View(model);
                }

                var compiledContractObject = JsonConvert.DeserializeObject<object[]>(compiledContractString);
                model.Abi = JsonConvert.SerializeObject(compiledContractObject[0]);
                model.ByteCode = (string)compiledContractObject[1];

                model.Verified = true;
                return View(model);
            }
            else
            {
                var user = _userService.GetUsers(u => u.FirstName + " " + u.LastName == HttpContext.User.Identity.Name).FirstOrDefault();
                _smartContractService.SaveTemplate(new SmartContract
                {
                    Id = Guid.NewGuid(),
                    User = user,
                    Abi = model.Abi,
                    ByteCode = model.ByteCode
                });

                return RedirectToAction("Index");
                //try
                //{
                //    // TODO GET_PRIVATE_KEY
                //    await _smartContractService.DeployContract(new SmartContract { Abi = model.Abi, ByteCode = model.ByteCode, }, "0xb5b1870957d373ef0eeffecc6e4812c0fd08f554b37b233526acc331bf1544f7", 0);
                //    return RedirectToAction("Index");
                //}
                //catch (Exception ex)
                //{
                //    model.ErrorMessage = ex.Message;

                //    return View(model);
                //}
            }
        }
    }
}