using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SmarTreaty.Common.DomainModel;

namespace SmarTreaty.Common.Core.Services.Interfaces
{
    public interface IRoleService : IBaseService
    {
        IEnumerable<Role> GetRoles(Expression<Func<Role, bool>> predicate = null,
            Func<IQueryable<Role>, IOrderedQueryable<Role>> orderBy = null,
            string properties = "");

        Role GetRole(int id);
        void AddRole(Role role);
        void UpdateRole(Role role);
        void DeleteRole(Role role);
        void DeleteRole(int id);
    }
}