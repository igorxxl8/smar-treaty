using System;
using System.Collections.Generic;

namespace SmarTreaty.Common.DomainModel
{
    public class Trainer : Entity<Guid>
    {
        public string Info { get; set; }
        public virtual User User { get; set; }
        public Guid TrainerGroupId { get; set; }
        public virtual TrainerGroup TrainerGroup { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}