using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SmarTreaty.Common.DomainModel;

namespace SmarTreaty.Core.Services.Interfaces
{
    public interface ITrainerService : IBaseService
    {
        IEnumerable<Trainer> GetTrainers(Expression<Func<Trainer, bool>> predicate = null,
            Func<IQueryable<Trainer>, IOrderedQueryable<Trainer>> orderBy = null,
            string properties = "");

        Trainer GetTrainer(Guid id);
        void AddTrainer(Trainer trainer);
        void UpdateTrainer(Trainer trainer);
        void DeleteTrainer(Trainer trainer);
        void DeleteTrainer(Guid id);
    }
}