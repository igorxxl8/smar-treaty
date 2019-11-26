using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SmarTreaty.Common.Core.Helpers.Interfaces;
using SmarTreaty.Common.DomainModel;
using SmarTreaty.Core.Services.Interfaces;

namespace SmarTreaty.Business.Services
{
    public class TrainerGroupService : BaseService, ITrainerGroupService
    {
        public TrainerGroupService(IDatabaseWorkUnit db) : base(db)
        {
        }

        public IEnumerable<TrainerGroup> GetTrainerGroups(Expression<Func<TrainerGroup, bool>> predicate = null,
            Func<IQueryable<TrainerGroup>, IOrderedQueryable<TrainerGroup>> orderBy = null,
            string properties = "")
        {
            if (predicate == null && orderBy == null && properties == "")
            {
                return Db.TrainerGroups.GetAll();
            }

            return Db.TrainerGroups.Get(predicate, orderBy, properties);
        }

        public TrainerGroup GetTrainerGroup(Guid id)
        {
            return Db.TrainerGroups.GetById(id);
        }

        public void AddTrainerGroup(TrainerGroup trainerGroup)
        {
            Db.TrainerGroups.Add(trainerGroup);
        }

        public void UpdateTrainerGroup(TrainerGroup trainerGroup)
        {
            Db.TrainerGroups.Update(trainerGroup);
        }

        public void DeleteTrainerGroup(TrainerGroup trainerGroup)
        {
            Db.TrainerGroups.Remove(trainerGroup);
        }

        public void DeleteTrainerGroup(Guid id)
        {
            Db.TrainerGroups.Remove(id);
        }
    }
}