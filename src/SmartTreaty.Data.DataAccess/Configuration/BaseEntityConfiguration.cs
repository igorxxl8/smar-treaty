using SmarTreaty.Common.DomainModel;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SmarTreaty.Business.Data.Configuration
{
    public class BaseEntityConfiguration<T> : EntityTypeConfiguration<T> where T : Entity<Guid>
    {
        protected BaseEntityConfiguration()
        {
            HasKey(e => e.Id);
            Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}