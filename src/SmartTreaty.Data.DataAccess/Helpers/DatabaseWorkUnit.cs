using SmarTreaty.Business.Data.Context;
using SmarTreaty.Business.Data.Repository;
using SmarTreaty.Common.Core.Helpers.Interfaces;
using SmarTreaty.Common.Core.Repository.Interfaces;
using SmarTreaty.Common.DomainModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace SmartTreaty.Data.DataAccess.Helpers
{
    public class DatabaseWorkUnit : IDatabaseWorkUnit
    {
        private readonly DataContext _context;
        private IRepository<User, Guid> _userRepository;
        private IRepository<Role, int> _roleRepository;

        public DatabaseWorkUnit(string connectionString)
        {
            _context = new DataContext(connectionString);
        }

        public IRepository<User, Guid> Users =>
            _userRepository ?? (_userRepository = new Repository<User, Guid>(_context));

        public IRepository<Role, int> Roles =>
            _roleRepository ?? (_roleRepository = new Repository<Role, int>(_context));

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Discard()
        {
            foreach (var entry in _context.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                }
            }
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
