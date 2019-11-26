using SmarTreaty.Common.DomainModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmarTreaty.ViewModels.CourseGroups
{
    public class IndexCourseGroupViewModel
    {
        public IndexCourseGroupViewModel(CourseGroup courseGroup)
        {
            Id = courseGroup.Id;
            Name = courseGroup.Name;
            Courses = courseGroup.Courses;
        }

        public Guid Id { get; set; }

        [Display(Name = "Title")]
        public string Name { get; set; }

        public ICollection<Course> Courses { get; set; }
    }
}