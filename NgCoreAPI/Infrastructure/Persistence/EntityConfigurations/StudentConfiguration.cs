using Infrastructure.Persistence.EntityConfigurations.BaseConfigurations;

namespace Infrastructure.Persistence.EntityConfigurations;

public class StudentConfiguration : BaseEntityConfiguration<Student>
{
    public override void Configure(EntityTypeBuilder<Student> entityTypeBuilder)
    {
        base.Configure(entityTypeBuilder);
        entityTypeBuilder.Property(x => x.StudentID).HasMaxLength(30).IsRequired();
        entityTypeBuilder.Property(x => x.StudentName).HasMaxLength(100).IsRequired();
        entityTypeBuilder.Property(x => x.FathersName).HasMaxLength(100).IsRequired();
        entityTypeBuilder.Property(x => x.Mobile).HasMaxLength(30).IsRequired();
        entityTypeBuilder.Property(x => x.Email).HasMaxLength(200).IsRequired();
        entityTypeBuilder.Property(x => x.PostalAddress).HasMaxLength(300).IsRequired();
        entityTypeBuilder.Property(x => x.DepartmentId).IsRequired();
        entityTypeBuilder.HasOne(x => x.Department).WithMany(a => a.StudentList).OnDelete(DeleteBehavior.Restrict).HasForeignKey(x => x.DepartmentId).OnDelete(DeleteBehavior.Restrict);
    }
}
