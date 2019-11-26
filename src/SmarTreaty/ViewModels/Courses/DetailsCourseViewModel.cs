using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SmarTreaty.Common.DomainModel;
using SmarTreaty.Enums;

namespace SmarTreaty.ViewModels.Courses
{
    public class DetailsCourseViewModel
    {
        public DetailsCourseViewModel()
        {
        }

        public DetailsCourseViewModel(Course course)
        {
            Name = course.Name;
            Description = course.Description;
            TypeCode = (CourseTypeEnum) course.TypeCode;
            PlanningMethodCode = (CoursePlanningMethodEnum) course.PlanningMethodCode;
            Trainers = course.Trainers;
        }

        public string Name { get; set; }
        public string Description { get; set; }

        [Display(Name = "Trainer Type")]
        public CourseTypeEnum TypeCode { get; set; }

        [Display(Name = "Arrangement")]
        public CoursePlanningMethodEnum PlanningMethodCode { get; set; }

        public ICollection<Trainer> Trainers { get; set; }
    }
}