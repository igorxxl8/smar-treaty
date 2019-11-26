using System;
using System.Collections.Generic;

namespace SmarTreaty.Common.DomainModel
{
    public class Course : Entity<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsNew { get; set; }
        public int TypeCode { get; set; }
        public int PlanningMethodCode { get; set; }
        public virtual ICollection<Trainer> Trainers { get; set; }
        public Guid CourseGroupId { get; set; }
        public virtual CourseGroup CourseGroup { get; set; }
    }
}