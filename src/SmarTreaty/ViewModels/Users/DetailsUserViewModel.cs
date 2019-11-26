using System;
using System.ComponentModel.DataAnnotations;
using SmarTreaty.Common.DomainModel;

namespace SmarTreaty.ViewModels.Users
{
    public class DetailsUserViewModel
    {
        public DetailsUserViewModel(User user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            StartDate = user.StartDate;
            EndDate = user.EndDate;
            Department = user.Department;
            Location = user.Location;
            Position = user.Position;
            Photo = user.Photo;
        }

        public Guid Id { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string Email { get; set; }
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string Position { get; set; }
        public byte[] Photo { get; set; }
    }
}