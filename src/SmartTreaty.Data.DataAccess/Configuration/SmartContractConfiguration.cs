using SmarTreaty.Common.DomainModel;
using System.Data.Entity.ModelConfiguration;

namespace SmartTreaty.Data.DataAccess.Configuration
{
    public class SmartContractConfiguration : EntityTypeConfiguration<SmartContract>
    {
        public SmartContractConfiguration()
        {
            HasKey(s => s.Id);
            Property(s => s.Abi);
            Property(s => s.ByteCode);
        }
    }
}
