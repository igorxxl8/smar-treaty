using System;
using SmarTreaty.Common.DomainModel;

namespace SmarTreaty.ViewModels.Trainers
{
    public class IndexTrainerViewModel
    {
        public IndexTrainerViewModel(Trainer trainer)
        {
            Id = trainer.Id;
            Info = trainer.Info;
            User = trainer.User;
            TrainerGroup = trainer.TrainerGroup;
        }

        public Guid Id { get; set; }
        public string Info { get; set; }
        public User User { get; set; }
        public TrainerGroup TrainerGroup { get; set; }
    }
}