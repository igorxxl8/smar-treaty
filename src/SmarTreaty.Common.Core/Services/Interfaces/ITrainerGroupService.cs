using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SmarTreaty.Common.DomainModel;

namespace SmarTreaty.Core.Services.Interfaces
{
    public interface ITrainerGroupService : IBaseService
    {
        IEnumerable<TrainerGroup> GetTrainerGroups(Expression<Func<TrainerGroup, bool>> predicate = null,
            Func<IQueryable<TrainerGroup>, IOrderedQueryable<TrainerGroup>> orderBy = null,
            string properties = "");

        TrainerGroup GetTrainerGroup(Guid id);
        void AddTrainerGroup(TrainerGroup trainerGroup);
        void UpdateTrainerGroup(TrainerGroup trainerGroup);
        void DeleteTrainerGroup(TrainerGroup trainerGroup);
        void DeleteTrainerGroup(Guid id);
    }
}