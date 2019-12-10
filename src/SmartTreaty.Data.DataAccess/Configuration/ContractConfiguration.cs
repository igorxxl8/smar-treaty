using SmarTreaty.Common.DomainModel;
using System.Data.Entity.ModelConfiguration;

namespace SmartTreaty.Data.DataAccess.Configuration
{
    public class ContractConfiguration : EntityTypeConfiguration<Contract>
    {
        public ContractConfiguration()
        {
            HasKey(c => c.Id);
            Property(c => c.Name);
            Property(c => c.Address);
            Property(c => c.CreationDate);
            HasRequired(c => c.User).WithMany(u => u.Contracts);
        }
    }
}
