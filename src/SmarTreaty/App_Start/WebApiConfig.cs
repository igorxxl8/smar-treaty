using System.Web.Http;
using SmarTreaty.App_Start;
using SmarTreaty.Helpers;

namespace SmarTreaty
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.DependencyResolver = new NinjectResolver(NinjectWebCommon.bootstrapper.Kernel);

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
