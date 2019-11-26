using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using SmarTreaty.Common.DomainModel;
using SmarTreaty.Core.Services.Interfaces;
using SmarTreaty.Enums;
using SmarTreaty.Helpers;
using SmarTreaty.ViewModels.Courses;

namespace SmarTreaty.Areas.admin.Controllers
{
    [RoutePrefix("courses")]
    [Route("{action}/{id}")]
    public class CoursesController : AdminController
    {
        private readonly ICourseService _courseService;
        private readonly ICourseGroupService _courseGroupService;
        private readonly ITrainerService _trainerService;

        public CoursesController(ICourseService courseService, ICourseGroupService courseGroupService, ITrainerService trainerService)
        {
            _courseService = courseService;
            _courseGroupService = courseGroupService;
            _trainerService = trainerService;
        }

        [Route("")]
        public ActionResult Index(Guid? courseGroups, string search, int? type, int? arrangement)
        {
            var codesItems = EnumHelper.GetSelectList(typeof(CourseTypeEnum));
            codesItems.Insert(0, new SelectListItem { Text = "All Types" });
            ViewBag.TypeCodes = new SelectList(codesItems, "Value", "Text");
            var methodsItems = EnumHelper.GetSelectList(typeof(CoursePlanningMethodEnum));
            methodsItems.Insert(0, new SelectListItem { Text = "All Arrangements" });
            ViewBag.PlanningMethods = new SelectList(methodsItems, "Value", "Text");
            var groupsItems =_courseGroupService.GetCourseGroups().ToList();
            groupsItems.Insert(0, new CourseGroup{Name="All groups"});
            ViewBag.CourseGroupsList = new SelectList(groupsItems, "Id", "Name");

            var predicate = PredicateBuilder.True<Course>();
            if (courseGroups != null && courseGroups != Guid.Empty)
            {
                predicate = predicate.And(c => c.CourseGroupId == courseGroups);
            }

            if (!string.IsNullOrEmpty(search))
            {
                predicate = predicate.And(c => c.Name.Contains(search));
            }

            if (type != null)
            {
                predicate = predicate.And(c => c.TypeCode == type);
            }

            if (arrangement != null)
            {
                predicate = predicate.And(c => c.PlanningMethodCode == arrangement);
            }

            var courses = _courseService.GetCourses(null, properties: "Trainers")
                .Select(m => new IndexCourseViewModel(m));
            return View(courses);
        }

        [Route("{id}")]
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var course = _courseService.GetCourse(id.Value);
            if (course == null)
            {
                return HttpNotFound();
            }

            var detailsCourseVm = new DetailsCourseViewModel(course);
            return View(detailsCourseVm);
        }

        [Route("create")]
        public ActionResult Create()
        {
            ViewBag.CourseGroupsList = new SelectList(_courseGroupService.GetCourseGroups(), "Id", "Name");
            var model = new CreateCourseViewModel();
            return View(model);
        }

        [Route("create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateCourseViewModel courseViewModel)
        {
            if (ModelState.IsValid)
            {
                _courseService.AddCourse(courseViewModel.GetCourse());
                _courseService.Save();
                return RedirectToAction("Index");
            }

            ViewBag.CourseGroupsList = new SelectList(_courseGroupService.GetCourseGroups(), "Id", "Name");
            return View(courseViewModel);
        }

        [Route("edit/{id}")]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var course = _courseService.GetCourse(id.Value);
            if (course == null)
            {
                return HttpNotFound();
            }

            var editCourseVm = new EditCourseViewModel(course);
            ViewBag.CourseGroupsList = new SelectList(_courseGroupService.GetCourseGroups(), "Id", "Name");
            return View(editCourseVm);
        }

        [Route("edit/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditCourseViewModel editCourseVm)
        {
            if (ModelState.IsValid)
            {
                _courseService.UpdateCourse(editCourseVm.GetCourse());
                _courseService.Save();
                return RedirectToAction("Index");
            }

            ViewBag.CourseGroupsList = new SelectList(_courseGroupService.GetCourseGroups(), "Id", "Name");
            return View(editCourseVm);
        }

        [Route("delete/{id}")]
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var course = _courseService.GetCourse(id.Value);
            if (course == null)
            {
                return HttpNotFound();
            }

            return View(course);
        }

        [Route("delete/{id}")]
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            _courseService.DeleteCourse(id);
            _courseService.Save();
            return RedirectToAction("Index");
        }

        [Route("{id}/trainers")]
        public ActionResult UpdateTrainersForCourse(Guid id)
        {
            var course = _courseService.GetCourse(id);

            if (course == null)
            {
                return HttpNotFound();
            }

            var listItems = _trainerService.GetTrainers()
                .Where(t => !course.Trainers.Contains(t))
                .Select(s => new { ID = s.Id, FullName = s.User.FirstName + " " + s.User.LastName });
            ViewBag.Trainers = new SelectList(listItems, "Id", "FullName");
            var vm = new UpdateTrainersForCourseViewModel(course);
            return View(vm);
        }
    }
}