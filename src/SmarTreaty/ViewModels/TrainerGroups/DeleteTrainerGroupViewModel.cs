using SmarTreaty.Common.DomainModel;
using System.ComponentModel.DataAnnotations;

namespace SmarTreaty.ViewModels.TrainerGroups
{
    public class DeleteTrainerGroupViewModel
    {
        public DeleteTrainerGroupViewModel(TrainerGroup trainerGroup)
        {
            Name = trainerGroup.Name;
        }

        [Display(Name = "Title")]
        public string Name { get; set; }
    }
}