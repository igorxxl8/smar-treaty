using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using SmarTreaty.App_Start;
using SmarTreaty.Common.Core.Services.Interfaces;
using SmarTreaty.Helpers;

namespace SmarTreaty
{
    public class MvcApplication : HttpApplication
    {
        private readonly NinjectResolver _ninjectResolver = new NinjectResolver(NinjectWebCommon.bootstrapper.Kernel);

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.Name;
        }

        protected void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            if (!FormsAuthentication.CookiesSupported)
            {
                return;
            }

            if (Request.Cookies[FormsAuthentication.FormsCookieName] == null)
            {
                return;
            }

            try
            {
                var login = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value)?.Name;
                var userService = (IUserService) _ninjectResolver.GetService(typeof(IUserService));
                var user = userService.GetUser(login);
                if (user == null)
                {
                    return;
                }

                var roles = user.Roles;
                var name = user.FirstName + " " + user.LastName;

                if (roles != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, name, ClaimValueTypes.String, null),
                        new Claim(ClaimTypes.Email, login, ClaimValueTypes.String, null),
                    };
                    foreach (var role in roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role.Name));
                    }

                    HttpContext.Current.User = new ClaimsPrincipal(new ClaimsIdentity(claims, "ApplicationCookie" , ClaimTypes.Name, ClaimTypes.Role));
                }
            }
            catch (Exception)
            {
                //somehting went wrong
            }
        }
    }
}