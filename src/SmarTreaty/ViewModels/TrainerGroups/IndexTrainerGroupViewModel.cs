using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SmarTreaty.Common.DomainModel;

namespace SmarTreaty.ViewModels.TrainerGroups
{
    public class IndexTrainerGroupViewModel
    {
        public IndexTrainerGroupViewModel(TrainerGroup trainerGroup, ICollection<Trainer> trainers)
        {
            Id = trainerGroup.Id;
            Name = trainerGroup.Name;
            Trainers = trainers;
        }

        public IndexTrainerGroupViewModel(TrainerGroup trainerGroup)
        {
            Id = trainerGroup.Id;
            Name = trainerGroup.Name;
            Trainers = trainerGroup.Trainers;
        }

        public Guid Id { get; set; }

        [Display(Name = "Title")]
        public string Name { get; set; }

        public ICollection<Trainer> Trainers { get; set; }
    }
}