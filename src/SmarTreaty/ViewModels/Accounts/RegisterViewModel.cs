using SmarTreaty.Common.DomainModel;
using System;
using System.ComponentModel.DataAnnotations;

namespace SmarTreaty.ViewModels.Accounts
{
    public class RegisterViewModel
    {
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get;  set; }
        public string Password { get; set; }
        
        [Compare("Password")]
        public string PasswordConfirmation { get; set; }
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