using SmarTreaty.Common.DomainModel;
using System.ComponentModel.DataAnnotations;

namespace SmarTreaty.ViewModels.CourseGroups
{
    public class DeleteCourseGroupViewModel
    {
        public DeleteCourseGroupViewModel(CourseGroup courseGroup)
        {
            Name = courseGroup.Name;
        }

        [Display(Name = "Title")]
        public string Name { get; set; }
    }
}