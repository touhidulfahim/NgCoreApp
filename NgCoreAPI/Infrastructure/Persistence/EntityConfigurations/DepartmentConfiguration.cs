using Infrastructure.Persistence.EntityConfigurations.BaseConfigurations;

namespace Infrastructure.Persistence.EntityConfigurations;

public class DepartmentConfiguration : BaseEntityConfiguration<Department>
{
    public override void Configure(EntityTypeBuilder<Department> entityTypeBuilder)
    {
        base.Configure(entityTypeBuilder);

        entityTypeBuilder.Property(x => x.DepartmentCode).HasMaxLength(10).IsRequired();
        entityTypeBuilder.Property(x => x.DepartmentName).HasMaxLength(250).IsRequired();

    }
}
