using SmarTreaty.Common.DomainModel;
using System;
using System.Collections.Generic;

namespace SmarTreaty.ViewModels.Courses
{
    public class UpdateTrainersForCourseViewModel
    {
        public UpdateTrainersForCourseViewModel(Course course)
        {
            Id = course.Id;
            Name = course.Name;
            Trainers = course.Trainers;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Trainer> Trainers { get; set; }
    }
}