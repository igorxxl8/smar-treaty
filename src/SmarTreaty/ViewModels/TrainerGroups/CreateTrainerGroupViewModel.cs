using SmarTreaty.Common.DomainModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmarTreaty.ViewModels.TrainerGroups
{
    public class CreateTrainerGroupViewModel
    {
        [Required]
        [Display(Name = "Title")]
        [MaxLength(32)]
        public string Name { get; set; }

        public TrainerGroup GetTrainerGroup()
        {
            return new TrainerGroup
            {
                Name = Name,
                Trainers = new List<Trainer>()
            };
        }
    }
}