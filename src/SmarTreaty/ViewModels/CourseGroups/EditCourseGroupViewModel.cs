using SmarTreaty.Common.DomainModel;
using System;
using System.ComponentModel.DataAnnotations;

namespace SmarTreaty.ViewModels.CourseGroups
{
    public class EditCourseGroupViewModel
    {
        public EditCourseGroupViewModel()
        {
        }

        public EditCourseGroupViewModel(CourseGroup courseGroup)
        {
            Id = courseGroup.Id;
            Name = courseGroup.Name;
        }

        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Title")]
        [MaxLength(32)]
        public string Name { get; set; }

        public CourseGroup GetCourseGroup()
        {
            return new CourseGroup
            {
                Id = Id,
                Name = Name
            };
        }
    }
}