using System;
using System.Collections.Generic;

namespace SmarTreaty.Common.DomainModel
{
    public class CourseGroup : Entity<Guid>
    {
        public string Name { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}