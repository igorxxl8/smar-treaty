using System;
using System.Collections.Generic;

namespace SmarTreaty.Common.DomainModel
{
    public class TrainerGroup : Entity<Guid>
    {
        public string Name { get; set; }
        public virtual ICollection<Trainer> Trainers { get; set; }
    }
}