using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SmarTreaty.Common.DomainModel;

namespace SmarTreaty.Common.ViewModels.Users
{
    public class DetailsUserViewModel
    {
        public DetailsUserViewModel(User user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            MiddleName = user.MiddleName;
            Login = user.Login;
            RegistrationDate = user.RegistrationDate;
            PrivateKey = user.PrivateKey;
            Contracts = user.Contracts;
        }

        public Guid Id { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }
        public string Login { get; set; }
        [Display(Name = "Registration Date")]
        [DataType(DataType.Date)]
        public DateTime RegistrationDate { get; set; }
        [Display(Name = "Wallet Private Key")]
        public string PrivateKey { get; set; }

        public ICollection<Contract> Contracts { get; set; }
    }
}