using SmarTreaty.Common.DomainModel;

namespace SmarTreaty.Business.Data.Configuration
{
    public class CourseConfiguration : BaseEntityConfiguration<Course>
    {
        private const int NameMaxLength = 64;
        private const int DescriptionMaxLength = 4000;

        public CourseConfiguration()
        {
            Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(NameMaxLength);

            Property(c => c.Description)
                .IsOptional()
                .HasMaxLength(DescriptionMaxLength);

            Property(c => c.IsNew)
                .IsRequired();

            Property(c => c.TypeCode)
                .IsRequired();

            Property(c => c.PlanningMethodCode)
                .IsRequired();

            HasMany(c => c.Trainers)
                .WithMany(t => t.Courses)
                .Map(ct =>
                {
                    ct.ToTable("TrainerCourse");
                    ct.MapLeftKey("Course_Id");
                    ct.MapRightKey("Trainer_Id");
                });

            HasRequired(c => c.CourseGroup)
                .WithMany(cg => cg.Courses)
                .HasForeignKey(c => c.CourseGroupId);
        }
    }
}