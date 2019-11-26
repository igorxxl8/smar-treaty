using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SmarTreaty.Common.Core.Helpers.Interfaces;
using SmarTreaty.Common.DomainModel;
using SmarTreaty.Core.Services.Interfaces;

namespace SmarTreaty.Business.Services
{
    public class UserService : BaseService, IUserService
    {
        public UserService(IDatabaseWorkUnit db) : base(db)
        {
        }

        public IEnumerable<User> GetUsers(Expression<Func<User, bool>> predicate = null,
            Func<IQueryable<User>, IOrderedQueryable<User>> orderBy = null,
            string properties = "")
        {
            if (predicate == null && orderBy == null && properties == "")
            {
                return Db.Users.GetAll();
            }

            return Db.Users.Get(predicate, orderBy, properties);
        }

        public User GetUser(Guid id)
        {
            return Db.Users.GetById(id);
        }

        public void AddUser(User user)
        {
            Db.Users.Add(user);
        }

        public void UpdateUser(User user)
        {
            Db.Users.Update(user);
        }

        public void DeleteUser(User user)
        {
            Db.Users.Remove(user);
        }

        public void DeleteUser(Guid id)
        {
            Db.Users.Remove(id);
        }

        public User GetUser(string email)
        {
            return Db.Users.Get(u => u.Email == email, properties:"Roles,Trainer").First();
        }

        public string TryLogin(string email, string password, Func<string,string,string> hashingCallback)
        {
            var users = GetUsers(u => u.Email == email)
                .Where(u => u.PasswordHash == hashingCallback(password, u.PasswordSalt));

            if (users.Any())
            {
                return null;
            }

            return "Incorrect email or password!";
        }
    }
}