using SmarTreaty.Common.DomainModel;
using System;
using System.ComponentModel.DataAnnotations;

namespace SmarTreaty.Common.ViewModels.Accounts
{
    public class RegisterViewModel
    {
        [Required]
        public string Login { get; set; }

        [Required(ErrorMessage = "The First Name field is required!")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "The Last Name field is required!")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "The Middle Name field is required!")]
        public string MiddleName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "The Password Confirmation field is required!")]
        [Compare("Password", ErrorMessage = "Passwords are not equal!")]
        [DataType(DataType.Password)]
        public string PasswordConfirmation { get; set; }

        [Required]
        public string Wallet { get; set; }

        public User GetUser()
        {
            return new User
            {
                Login = Login,
                FirstName = FirstName,
                LastName = LastName,
                MiddleName = MiddleName,
                RegistrationDate = DateTime.Now,
                Wallet = Wallet
            };
        }
    }
}