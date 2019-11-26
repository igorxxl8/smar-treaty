using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using SmarTreaty.Common.DomainModel;

namespace SmarTreaty.Business.Data.Configuration
{
    public class CourseGroupConfiguration : BaseEntityConfiguration<CourseGroup>
    {
        private const int MaxLength = 32;

        public CourseGroupConfiguration()
        {
            Property(cg => cg.Name)
                .IsRequired()
                .HasMaxLength(MaxLength)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("IX_Name") {IsUnique = true}));
        }
    }
}