using SmarTreaty.Common.DomainModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmarTreaty.ViewModels.CourseGroups
{
    public class CreateCourseGroupViewModel
    {
        [Required]
        [MaxLength(32)]
        [Display(Name = "Title")]
        public string Name { get; set; }

        public CourseGroup GetCourseGroup()
        {
            return new CourseGroup
            {
                Name = Name,
                Courses = new List<Course>()
            };
        }
    }
}