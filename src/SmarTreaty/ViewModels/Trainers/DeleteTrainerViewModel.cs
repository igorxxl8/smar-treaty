using SmarTreaty.Common.DomainModel;

namespace SmarTreaty.ViewModels.Trainers
{
    public class DeleteTrainerViewModel
    {
        public DeleteTrainerViewModel(Trainer trainer)
        {
            User = trainer.User;
            TrainerGroup = trainer.TrainerGroup;
            Info = trainer.Info;
        }

        public User User { get; set; }
        public TrainerGroup TrainerGroup { get; set; }
        public string Info { get; set; }
    }
}