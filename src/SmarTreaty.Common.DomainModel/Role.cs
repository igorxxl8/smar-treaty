using System.Collections.Generic;

namespace SmarTreaty.Common.DomainModel
{
    public class Role : Entity<int>
    {
        public string Name { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}