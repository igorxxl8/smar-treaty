using SmarTreaty.Common.DomainModel;
using System.Data.Entity.ModelConfiguration;

namespace SmartTreaty.Data.DataAccess.Configuration
{
    public class ContractConfiguration : EntityTypeConfiguration<Contract>
    {
        public ContractConfiguration()
        {
            HasKey(c => c.Id);
            Property(c => c.Address);
        }
    }
}
