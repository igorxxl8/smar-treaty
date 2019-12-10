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

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(object templateViewModel)
        {
            // return validation error if not verified

            try
            {
                //add template

                return RedirectToAction("Index");
            }
            catch
            {
                return HttpNotFound();
            }
        }
    }
}