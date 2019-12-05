using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmarTreaty.Areas.editor.Controllers
{
    [RoutePrefix("templates")]
    [Route("{action}/{id}")]
    public class TemplatesController : EditorController
    {

        public TemplatesController()
        {

        }

        [Route("")]
        public ActionResult Index()
        {
            return View();
        }
    }
}