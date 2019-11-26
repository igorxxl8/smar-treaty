using System;
using System.ComponentModel.DataAnnotations;
using SmarTreaty.Common.DomainModel;

namespace SmarTreaty.ViewModels.TrainerGroups
{
    public class EditTrainerGroupViewModel
    {
        public EditTrainerGroupViewModel()
        {
        }

        public EditTrainerGroupViewModel(TrainerGroup trainerGroup)
        {
            Id = trainerGroup.Id;
            Name = trainerGroup.Name;
        }

        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Title")]
        [MaxLength(32)]
        public string Name { get; set; }

        public TrainerGroup GetTrainerGroup()
        {
            return new TrainerGroup
            {
                Id = Id,
                Name = Name
            };
        }
    }
}