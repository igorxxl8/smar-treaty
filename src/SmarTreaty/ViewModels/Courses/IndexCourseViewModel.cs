using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SmarTreaty.Common.DomainModel;
using SmarTreaty.Enums;

namespace SmarTreaty.ViewModels.Courses
{
    public class IndexCourseViewModel
    {
        public IndexCourseViewModel()
        {
        }

        public IndexCourseViewModel(Course course)
        {
            Id = course.Id;
            Name = course.Name;
            Trainers = course.Trainers;
            TypeCode = (CourseTypeEnum) course.TypeCode;
            PlanningMethodCode = (CoursePlanningMethodEnum) course.PlanningMethodCode;
            Description = course.Description;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Trainer> Trainers { get; set; }

        [Display(Name = "Training Type")]
        public CourseTypeEnum TypeCode { get; set; }

        [Display(Name = "Arrangement")]
        public CoursePlanningMethodEnum PlanningMethodCode { get; set; }

        public string Description { get; set; }
    }
}