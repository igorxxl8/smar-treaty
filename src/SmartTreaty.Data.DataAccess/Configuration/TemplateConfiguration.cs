using SmarTreaty.Common.DomainModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Text;

namespace SmartTreaty.Data.DataAccess.Configuration
{
    public class TemplateConfiguration : EntityTypeConfiguration<Template>
    {
        public TemplateConfiguration()
        {
            HasKey(c => c.Id);
            Property(c => c.Abi);
            Property(c => c.ByteCode);
            HasRequired(c => c.User);
        }
    }
}
