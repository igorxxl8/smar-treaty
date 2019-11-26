using System.Web.Mvc;

namespace SmarTreaty.Controllers
{
    [RoutePrefix("home")]
    public class HomeController : DefaultController
    {
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }
    }
}