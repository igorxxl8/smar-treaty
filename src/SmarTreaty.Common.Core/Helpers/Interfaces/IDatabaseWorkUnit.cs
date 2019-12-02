using System;
using SmarTreaty.Common.DomainModel;
using SmarTreaty.Common.Core.Repository.Interfaces;

namespace SmarTreaty.Common.Core.Helpers.Interfaces
{
    public interface IDatabaseWorkUnit : IDisposable
    {
        IRepository<User, Guid> Users { get; }
        IRepository<Role, int> Roles { get; }
        void Save();
        void Discard();
    }
}