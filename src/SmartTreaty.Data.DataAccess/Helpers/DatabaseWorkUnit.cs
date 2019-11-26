using System;
using System.Data.Entity;
using SmarTreaty.Business.Data.Context;
using SmarTreaty.Business.Data.Repository;
using SmarTreaty.Common.Core.Helpers.Interfaces;
using SmarTreaty.Common.Core.Repository.Interfaces;
using SmarTreaty.Common.DomainModel;

namespace SmarTreaty.Business.Data.Helpers
{
    public class DatabaseWorkUnit : IDatabaseWorkUnit
    {
        private readonly DataContext _context;
        private IRepository<User, Guid> _userRepository;
        private IRepository<Course, Guid> _courseRepository;
        private IRepository<CourseGroup, Guid> _courseGroupRepository;
        private IRepository<Role, int> _roleRepository;
        private IRepository<TrainerGroup, Guid> _trainerGroupRepository;
        private IRepository<Trainer, Guid> _trainerRepository;

        public DatabaseWorkUnit(string connectionString)
        {
            _context = new DataContext(connectionString);
        }

        public IRepository<User, Guid> Users =>
            _userRepository ?? (_userRepository = new Repository<User, Guid>(_context));

        public IRepository<Role, int> Roles =>
            _roleRepository ?? (_roleRepository = new Repository<Role, int>(_context));

        public IRepository<Trainer, Guid> Trainers =>
            _trainerRepository ?? (_trainerRepository = new Repository<Trainer, Guid>(_context));

        public IRepository<Course, Guid> Courses =>
            _courseRepository ?? (_courseRepository = new Repository<Course, Guid>(_context));

        public IRepository<CourseGroup, Guid> CourseGroups =>
            _courseGroupRepository ?? (_courseGroupRepository = new Repository<CourseGroup, Guid>(_context));

        public IRepository<TrainerGroup, Guid> TrainerGroups =>
            _trainerGroupRepository ?? (_trainerGroupRepository = new Repository<TrainerGroup, Guid>(_context));


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