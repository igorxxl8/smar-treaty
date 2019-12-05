using SmarTreaty.Common.Core.Helpers.Interfaces;
using SmarTreaty.Common.Core.Services.Interfaces;
using SmarTreaty.Common.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SmarTreaty.Business.Services
{
    public class RoleService : BaseService, IRoleService
    {
        public RoleService(IDatabaseWorkUnit db) : base(db)
        {
        }

        public IEnumerable<Role> GetRoles(Expression<Func<Role, bool>> predicate = null,
            Func<IQueryable<Role>, IOrderedQueryable<Role>> orderBy = null,
            string properties = "")
        {
            if (predicate == null && orderBy == null && properties == "")
            {
                return Db.Roles.GetAll();
            }

            return Db.Roles.Get(predicate, orderBy, properties);
        }

        public Role GetRole(int id)
        {
            return Db.Roles.GetById(id);
        }

        public void AddRole(Role role)
        {
            Db.Roles.Add(role);
        }

        public void UpdateRole(Role role)
        {
            Db.Roles.Update(role);
        }

        public void DeleteRole(Role role)
        {
            Db.Roles.Remove(role);
        }

        public void DeleteRole(int id)
        {
            Db.Roles.Remove(id);
        }
    }
}
