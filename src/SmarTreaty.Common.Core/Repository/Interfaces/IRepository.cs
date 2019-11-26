using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SmarTreaty.Common.DomainModel;

namespace SmarTreaty.Common.Core.Repository.Interfaces
{
    public interface IRepository<T, in TId> where T : Entity<TId>
    {
        IEnumerable<T> GetAll();

        IEnumerable<T> Get(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string properties = "");

        T GetById(TId id);
        void Add(T item);
        void Update(T item);
        void Remove(T item);
        void Remove(TId id);
    }
}