using System.Web.Mvc;

namespace SmarTreaty.Controllers
{
    [RoutePrefix("contracts")]
    public class ContractsController : DefaultController
    {
        // GET: Contracts
        public ActionResult Index()
        {
            return View();
        }
    }
}