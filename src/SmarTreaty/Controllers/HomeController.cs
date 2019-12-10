using SmarTreaty.Common.Core.Services.Interfaces;
using SmarTreaty.Common.ViewModels.Users;
using SmarTreaty.Common.DomainModel;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System;

namespace SmarTreaty.Controllers
{
    [RoutePrefix("home")]
    public class HomeController : DefaultController
    {
        private readonly IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }


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
            user.Contracts = list;
            var vm = new DetailsUserViewModel(user);
            return View(vm);
        }
    }
}