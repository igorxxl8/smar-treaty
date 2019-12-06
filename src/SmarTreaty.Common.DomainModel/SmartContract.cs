using System;

namespace SmarTreaty.Common.DomainModel
{
    public class SmartContract : Entity<Guid>
    {
        public User User { get; set; }
        public string Abi { get; set; }
        public string ByteCode { get; set; }
    }
}
