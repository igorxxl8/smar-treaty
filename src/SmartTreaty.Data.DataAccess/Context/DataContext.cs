using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using SmarTreaty.Business.Data.Configuration;
using SmarTreaty.Common.DomainModel;

namespace SmarTreaty.Business.Data.Context
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(string connectionString) : base(connectionString)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new RoleConfiguration());
        }
    }
}