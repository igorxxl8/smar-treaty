using System;

namespace SmarTreaty.Common.DomainModel
{
    public class Contract : Entity<Guid>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime CreationDate { get; set; }
        public User User { get; set; }
    }
}
