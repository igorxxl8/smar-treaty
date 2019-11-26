using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using SmarTreaty.Business.Data.Context;
using SmarTreaty.Common.Core.Repository.Interfaces;
using SmarTreaty.Common.DomainModel;

namespace SmarTreaty.Business.Data.Repository
{
    public class Repository<T, TId> : IRepository<T, TId> where T : Entity<TId>
    {
        protected readonly DataContext Context;
        protected readonly DbSet<T> Entities;

        public Repository(DataContext context)
        {
            Context = context;
            Entities = Context.Set<T>();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return Entities.ToList();
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string properties = "")
        {
            IQueryable<T> query = Entities;
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            foreach (var property in properties.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(property);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }

            return query.ToList();
        }

        public virtual T GetById(TId id)
        {
            return Entities.Find(id);
        }

        public virtual void Add(T item)
        {
            Entities.Add(item);
        }

        public virtual void Update(T item)
        {
            Entities.AddOrUpdate(item);
        }

        public virtual void Remove(T item)
        {
            if (item == null)
            {
                return;
            }

            if (Context.Entry(item).State == EntityState.Detached)
            {
                Entities.Attach(item);
            }

            Entities.Remove(item);
        }

        public void Remove(TId id)
        {
            var entity = GetById(id);
            if (entity == null)
            {
                return;
            }

            Entities.Remove(entity);
        }
    }
}