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
        public DbSet<Course> Courses { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<CourseGroup> CourseGroups { get; set; }
        public DbSet<TrainerGroup> TrainerGroups { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new RoleConfiguration());
            modelBuilder.Configurations.Add(new TrainerConfiguration());
            modelBuilder.Configurations.Add(new TrainerGroupConfiguration());
            modelBuilder.Configurations.Add(new CourseConfiguration());
            modelBuilder.Configurations.Add(new CourseGroupConfiguration());
        }
    }
}