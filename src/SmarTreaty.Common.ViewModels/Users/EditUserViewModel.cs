using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SmarTreaty.Common.DomainModel;

namespace SmarTreaty.Common.ViewModels.Users
{
    public class EditUserViewModel //: IValidatableObject
    {
        public EditUserViewModel()
        {

        }

        public EditUserViewModel(User user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            MiddleName = user.MiddleName;
            RegistrationDate = user.RegistrationDate;
            Roles = user.Roles;
        }

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        [Display(Name = "Registration Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime RegistrationDate { get; set; }
        public ICollection<Role> Roles { get; set; }

        public void UpdateUser(User user)
        {
            user.RegistrationDate = RegistrationDate;
        }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    return null;
        //    //if (EndDate != null && EndDate < RegistrationDate)
        //    //{
        //    //    yield return new ValidationResult(errorMessage: "End Date must be greater than Start Date", memberNames: new [] {"EndDate"});
        //    //}
        //}
    }
}