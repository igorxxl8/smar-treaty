using SmarTreaty.Common.DomainModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;

namespace SmarTreaty.Business.Data.Configuration
{
    public class TrainerGroupConfiguration : BaseEntityConfiguration<TrainerGroup>
    {
        private const int MaxLength = 32;

        public TrainerGroupConfiguration()
        {
            Property(tg => tg.Name)
                .IsRequired()
                .HasMaxLength(MaxLength)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("IX_Name") {IsUnique = true}));
        }
    }
}