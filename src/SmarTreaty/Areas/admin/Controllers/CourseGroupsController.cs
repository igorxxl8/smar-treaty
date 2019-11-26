using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SmarTreaty.Core.Services.Interfaces;
using SmarTreaty.ViewModels.CourseGroups;

namespace SmarTreaty.Areas.admin.Controllers
{
    [RoutePrefix("coursegroups")]
    [Route("{action}/{id}")]
    public class CourseGroupsController : AdminController
    {
        private readonly ICourseGroupService _courseGroupService;

        public CourseGroupsController(ICourseGroupService courseGroupService)
        {
            _courseGroupService = courseGroupService;
        }

        [Route("")]
        public ActionResult Index()
        {
            var courseGroups = _courseGroupService.GetCourseGroups().Select(cg => new IndexCourseGroupViewModel(cg));
            return View(courseGroups);
        }

        [Route("create")]
        public ActionResult Create()
        {
            var vm = new CreateCourseGroupViewModel();
            return View(vm);
        }

        [Route("create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateCourseGroupViewModel courseGroupVm)
        {
            if (!ModelState.IsValid) return View(courseGroupVm);

            _courseGroupService.AddCourseGroup(courseGroupVm.GetCourseGroup());
            _courseGroupService.Save();
            return RedirectToAction("Index");
        }

        [Route("edit/{id}")]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var courseGroup = _courseGroupService.GetCourseGroup(id.Value);
            if (courseGroup == null)
            {
                return HttpNotFound();
            }

            var vm = new EditCourseGroupViewModel(courseGroup);
            return View(vm);
        }

        [Route("edit/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditCourseGroupViewModel courseGroup)
        {
            if (ModelState.IsValid)
            {
                _courseGroupService.UpdateCourseGroup(courseGroup.GetCourseGroup());
                _courseGroupService.Save();
                return RedirectToAction("Index");
            }

            return View(courseGroup);
        }

        [Route("delete/{id}")]
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var courseGroup = _courseGroupService.GetCourseGroup(id.Value);
            if (courseGroup == null)
            {
                return HttpNotFound();
            }

            var vm = new DeleteCourseGroupViewModel(courseGroup);
            return View(vm);
        }

        [Route("delete/{id}")]
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            _courseGroupService.DeleteCourseGroup(id);
            _courseGroupService.Save();
            return RedirectToAction("Index");
        }
    }
}