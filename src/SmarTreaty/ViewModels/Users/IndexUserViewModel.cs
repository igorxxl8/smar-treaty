using SmarTreaty.Common.DomainModel;
using System;

namespace SmarTreaty.ViewModels.Users
{
    public class IndexUserViewModel
    {
        public IndexUserViewModel(User user)
        {
            Id = user.Id;
            Name = user.FirstName + " " + user.LastName;
            Photo = user.Photo;
            Department = user.Department;
            Position = user.Position;
            Location = user.Location;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public byte[] Photo { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        public string Location { get; set; }
    }
}