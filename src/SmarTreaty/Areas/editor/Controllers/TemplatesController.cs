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

        [Route("create")]
        public ActionResult Create()
        {
            return View();
        }

        [Route("create")]
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}