using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SmarTreaty.Common.DomainModel;

namespace SmarTreaty.ViewModels.Users
{
    public class EditUserViewModel : IValidatableObject
    {
        public EditUserViewModel()
        {
            
        }

        public EditUserViewModel(User user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            StartDate = user.StartDate;
            EndDate = user.EndDate;
            Roles = user.Roles;
        }

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Display(Name = "Start Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Display(Name = "End Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }
        public ICollection<Role> Roles { get; set; }

        public void UpdateUser(User user)
        {
            user.StartDate = StartDate;
            user.EndDate = EndDate;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EndDate != null && EndDate < StartDate)
            {
                yield return new ValidationResult(errorMessage: "End Date must be greater than Start Date", memberNames: new [] {"EndDate"});
            }
        }
    }
}