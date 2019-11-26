using System.Web.Mvc;

namespace SmarTreaty.Controllers
{
    [Authorize]
    public abstract class DefaultController : Controller
    {
    }
}