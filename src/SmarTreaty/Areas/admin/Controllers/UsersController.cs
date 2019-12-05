using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PagedList;
using SmarTreaty.Common.DomainModel;
using SmarTreaty.Common.ViewModels.Users;
using SmarTreaty.Common.Core.Services.Interfaces;

namespace SmarTreaty.Areas.admin.Controllers
{
    [RoutePrefix("users")]
    [Route("{action}/{id}")]
    public class UsersController : AdminController
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public UsersController(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        [Route("")]
        public ActionResult Index(string search, int? page)
        {
            IEnumerable<User> users;
            if (string.IsNullOrEmpty(search))
            {
                users = _userService.GetUsers();
            }
            else
            {
                page = 1;
                users = _userService.GetUsers(u => u.LastName.Contains(search)
                                                   || u.FirstName.Contains(search));
            }

            ViewBag.CurrentFilter = search;

            const int pageSize = 5;
            var pageNumber = page ?? 1;
            return View(users.Select(u => new IndexUserViewModel(u)).ToPagedList(pageNumber, pageSize));
        }

        [Route("{id}")]
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = _userService.GetUser(id.Value);
            if (user == null)
            {
                return HttpNotFound();
            }

            var vm = new DetailsUserViewModel(user);
            return View(vm);
        }

        [Route("edit/{id}")]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = _userService.GetUser(id.Value);
            if (user == null)
            {
                return HttpNotFound();
            }

            ViewBag.Roles = _roleService.GetRoles();
            var vm = new EditUserViewModel(user);
            return View(vm);
        }

        [Route("edit/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditUserViewModel userVm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Roles = _roleService.GetRoles();
                return View(userVm);
            }

            var user = _userService.GetUser(userVm.Id);
            userVm.UpdateUser(user);
            _userService.UpdateUser(user);
            _userService.Save();
            return RedirectToAction("Index");
        }
    }
}