using SmarTreaty.Core.Services.Interfaces;
using SmarTreaty.Helpers;
using SmarTreaty.ViewModels.Accounts;
using System.Web.Mvc;
using System.Web.Security;

namespace SmarTreaty.Controllers
{
    [RoutePrefix("account")]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
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
            //ViewBag.CourseGroupsList = new SelectList(_courseGroupService.GetCourseGroups(), "Id", "Name");
            var model = new RegisterViewModel();
            return View(model);
        }

        [Route("register")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                _userService.AddUser(registerViewModel.GetUser());
                _userService.Save();
                return RedirectToAction("Login");
            }

            return View(registerViewModel);
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

            return RedirectToAction("Index", "Courses", new {area=""});
        }

        [Route("signout")]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "Account");
        }
    }
}