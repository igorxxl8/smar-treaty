using System;

namespace SmarTreaty.Common.DomainModel
{
    public class Contract : Entity<Guid>
    {
        public string Address { get; set; }
    }
}
