using SmarTreaty.Common.DomainModel;
using System;
using System.ComponentModel.DataAnnotations;

namespace SmarTreaty.Common.ViewModels.Users
{
    public class IndexUserViewModel
    {
        public IndexUserViewModel(User user)
        {
            Id = user.Id;
            Name = user.FirstName + " " + user.LastName + " " + user.MiddleName;
            Wallet = user.PrivateKey;
            RegistrationDate = user.RegistrationDate;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Wallet { get; set; }

        [Display(Name = "Registration Date")]
        [DataType(DataType.Date)]
        public DateTime RegistrationDate { get; set; }
    }
}