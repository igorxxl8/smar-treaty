using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using SmarTreaty.Common.DomainModel;

namespace SmarTreaty.Business.Data.Configuration
{
    public class UserConfiguration : BaseEntityConfiguration<User>
    {
        private const int MaxLength = 128;

        public UserConfiguration()
        {
            Property(u => u.Login)
                .IsRequired()
                .HasMaxLength(MaxLength)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("IX_Login") {IsUnique = true}));

            Property(u => u.PasswordHash)
                .IsRequired()
                .HasMaxLength(MaxLength);

            Property(u => u.PasswordSalt)
                .IsRequired()
                .HasMaxLength(MaxLength);

            Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(MaxLength);

            Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(MaxLength);

            Property(u => u.MiddleName)
                .IsRequired()
                .HasMaxLength(MaxLength);

            Property(u => u.RegistrationDate)
                .IsRequired()
                .HasColumnType("date");

            Property(u => u.PrivateKey)
                .HasColumnName("Wallet")
                .HasMaxLength(MaxLength);
        }
    }
}