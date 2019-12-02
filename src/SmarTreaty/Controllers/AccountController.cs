using SmarTreaty.Common.DomainModel;
using SmarTreaty.Core.Services.Interfaces;
using SmarTreaty.Helpers;
using SmarTreaty.Common.ViewModels.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace SmarTreaty.Controllers
{
    [RoutePrefix("account")]
    public class AccountController : Controller
    {
        private static readonly Random Random = new Random();
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public AccountController(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }
        
        // GET: Account
        [Route("login")]
        public ActionResult Login(string returnUrl)
        {
            return View();
        }

        [Route("register")]
        public ActionResult Register()
        {
            var rVm = new RegisterViewModel();
            return View(rVm);
        }

        [Route("register")]
        [HttpPost]
        public ActionResult Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(registerViewModel);
            }

            var user = registerViewModel.GetUser();
            var salt = RandomString(128);
            var hash = PasswordHashing.GetPasswordHash(registerViewModel.Password, salt);
            var userRole = _roleService.GetRoles(r => r.Name == "user").FirstOrDefault();
            user.Roles = new List<Role> { userRole };
            user.PasswordSalt = salt;
            user.PasswordHash = hash;
            try
            {
                _userService.AddUser(user);
                _userService.Save();
                return RedirectToAction("Login");
            }
            catch
            {
                ModelState.AddModelError("", $"User with login {user.Login} already exists!");
                return View(registerViewModel);
            }
        }

        [Route("login")]
        [HttpPost]
        public ActionResult Login(LoginViewModel userLoginVm)
        {
            if (!ModelState.IsValid)
            {
                return View(userLoginVm);
            }

            var errorStatus = _userService
                .TryLogin(userLoginVm.Email, userLoginVm.Password, PasswordHashing.GetPasswordHash);

            if (errorStatus != null)
            {
                ModelState.AddModelError("", errorStatus);
                return View(userLoginVm);
            }

            FormsAuthentication.SetAuthCookie(userLoginVm.Email, false);
            if (!string.IsNullOrEmpty(userLoginVm.ReturnUrl) && Url.IsLocalUrl(userLoginVm.ReturnUrl))
            {
                return Redirect(userLoginVm.ReturnUrl);
            }

            return RedirectToAction("Index", "Contracts", new {area=""});
        }

        [Route("signout")]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "Account");
        }

        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        }
    }
}