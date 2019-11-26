using SmarTreaty.Common.DomainModel;
using System;
using System.ComponentModel.DataAnnotations;

namespace SmarTreaty.ViewModels.Trainers
{
    public class CreateTrainerViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [MaxLength(4000)]
        public string Info { get; set; }

        [Required]
        [Display(Name = "Trainer Group")]
        public Guid TrainerGroupId { get; set; }

        public Trainer GetTrainer()
        {
            return new Trainer
            {
                Id = Id,
                Info = Info,
                TrainerGroupId = TrainerGroupId
            };
        }
    }
}