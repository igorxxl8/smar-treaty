using System.Web.Mvc;

namespace SmarTreaty.Controllers
{
    [RoutePrefix("contracts")]
    public class ContractsController : Controller
    {
        // GET: Contracts
        public ActionResult Index()
        {
            return View();
        }
    }
}