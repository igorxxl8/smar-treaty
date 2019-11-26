using System.Web.Mvc;

namespace SmarTreaty.Areas.admin.Controllers
{
    [RouteArea("admin")]
    [Authorize(Roles="admin")]
    public abstract class AdminController : Controller
    {
    }
}