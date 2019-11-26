using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SmarTreaty.Common.Core.Helpers.Interfaces;
using SmarTreaty.Common.DomainModel;
using SmarTreaty.Core.Services.Interfaces;

namespace SmarTreaty.Business.Services
{
    public class TrainerService : BaseService, ITrainerService
    {
        public TrainerService(IDatabaseWorkUnit db) : base(db)
        {
        }

        public IEnumerable<Trainer> GetTrainers(Expression<Func<Trainer, bool>> predicate = null,
            Func<IQueryable<Trainer>, IOrderedQueryable<Trainer>> orderBy = null,
            string properties = "")
        {
            if (predicate == null && orderBy == null && properties == "")
            {
                return Db.Trainers.GetAll();
            }

            return Db.Trainers.Get(predicate, orderBy, properties);
        }

        public Trainer GetTrainer(Guid id)
        {
            return Db.Trainers.GetById(id);
        }

        public void AddTrainer(Trainer trainer)
        {
            Db.Trainers.Add(trainer);
        }

        public void UpdateTrainer(Trainer trainer)
        {
            Db.Trainers.Update(trainer);
        }

        public void DeleteTrainer(Trainer trainer)
        {
            Db.Trainers.Remove(trainer);
        }

        public void DeleteTrainer(Guid id)
        {
            Db.Trainers.Remove(id);
        }
    }
}