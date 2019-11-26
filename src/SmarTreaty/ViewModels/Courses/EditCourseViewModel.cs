using System;
using System.ComponentModel.DataAnnotations;
using SmarTreaty.Common.DomainModel;
using SmarTreaty.Enums;

namespace SmarTreaty.ViewModels.Courses
{
    public class EditCourseViewModel
    {
        public EditCourseViewModel()
        {
        }

        public EditCourseViewModel(Course course)
        {
            Id = course.Id;
            Name = course.Name;
            IsNew = course.IsNew;
            TypeCode = (CourseTypeEnum) course.TypeCode;
            PlanningMethodCode = (CoursePlanningMethodEnum) course.PlanningMethodCode;
            CourseGroupId = course.CourseGroupId;
            Description = course.Description;
        }

        public Guid Id { get; set; }

        [Required] [MaxLength(64)] public string Name { get; set; }

        public bool IsNew { get; set; }

        [Display(Name = "Training Type")]
        public CourseTypeEnum TypeCode { get; set; }

        [Display(Name = "Arrangement")]
        public CoursePlanningMethodEnum PlanningMethodCode { get; set; }

        [Required]
        [Display(Name = "Course Group")]
        public Guid CourseGroupId { get; set; }

        [MaxLength(4000)] public string Description { get; set; }

        public Course GetCourse()
        {
            return new Course
            {
                Id = Id,
                Name = Name,
                IsNew = IsNew,
                TypeCode = (int) TypeCode,
                PlanningMethodCode = (int) PlanningMethodCode,
                CourseGroupId = CourseGroupId,
                Description = Description
            };
        }
    }
}