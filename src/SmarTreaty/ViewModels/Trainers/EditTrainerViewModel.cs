using System;
using System.ComponentModel.DataAnnotations;
using SmarTreaty.Common.DomainModel;

namespace SmarTreaty.ViewModels.Trainers
{
    public class EditTrainerViewModel
    {
        public EditTrainerViewModel()
        {
        }

        public EditTrainerViewModel(Trainer trainer)
        {
            Id = trainer.Id;
            User = trainer.User;
            Info = trainer.Info;
            TrainerGroupId = trainer.TrainerGroupId;
        }

        public Guid Id { get; set; }
        public User User { get; set; }
        public string Info { get; set; }

        [Required]
        [Display(Name = "Trainer Group")]
        public Guid TrainerGroupId { get; set; }

        public Trainer GetTrainer()
        {
            return new Trainer
            {
                Id = Id,
                User = User,
                Info = Info,
                TrainerGroupId = TrainerGroupId
            };
        }
    }
}