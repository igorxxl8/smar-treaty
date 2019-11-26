using System.Data.Entity.ModelConfiguration;
using SmarTreaty.Common.DomainModel;

namespace SmarTreaty.Business.Data.Configuration
{
    public class TrainerConfiguration : EntityTypeConfiguration<Trainer>
    {
        private const int InfoMaxLength = 4000;

        public TrainerConfiguration()
        {
            HasKey(t => t.Id);

            Property(t => t.Info)
                .HasMaxLength(InfoMaxLength);

            HasRequired(t => t.User)
                .WithOptional(u => u.Trainer);

            HasRequired(t => t.TrainerGroup)
                .WithMany(tg => tg.Trainers)
                .HasForeignKey(t => t.TrainerGroupId);
        }
    }
}