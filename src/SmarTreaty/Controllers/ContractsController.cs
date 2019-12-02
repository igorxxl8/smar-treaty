using System.Web.Mvc;

namespace SmarTreaty.Controllers
{
    [RoutePrefix("contracts")]
    public class ContractsController : Controller
    {
        [Route("")]
        // GET: Contracts
        public ActionResult Index()
        {
            return View();
        }
    }
}