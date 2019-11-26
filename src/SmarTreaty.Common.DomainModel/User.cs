using System;
using System.Collections.Generic;

namespace SmarTreaty.Common.DomainModel
{
    public class User : Entity<Guid>
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string Position { get; set; }
        public byte[] Photo { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
        public virtual Trainer Trainer { get; set; }
    }
}