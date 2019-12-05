using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SmarTreaty.Common.DomainModel;

namespace SmarTreaty.Common.Core.Services.Interfaces
{
    public interface IUserService : IBaseService
    {
        IEnumerable<User> GetUsers(Expression<Func<User, bool>> predicate = null,
            Func<IQueryable<User>, IOrderedQueryable<User>> orderBy = null,
            string properties = "");

        User GetUser(Guid id);
        User GetUser(string login);
        string TryLogin(string login, string password, Func<string, string, string> hashingCallback);
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
        void DeleteUser(Guid id);
    }
}