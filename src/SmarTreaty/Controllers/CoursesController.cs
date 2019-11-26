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

namespace SmarTreaty.Controllers
{
    [RoutePrefix("courses")]
    public class CoursesController : DefaultController
    {
        private readonly ICourseService _courseService;
        private readonly ICourseGroupService _courseGroupService;

        public CoursesController(ICourseService courseService, ICourseGroupService courseGroupService)
        {
            _courseService = courseService;
            _courseGroupService = courseGroupService;
        }

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

            predicate = predicate.And(c => c.IsNew);

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
    }
}