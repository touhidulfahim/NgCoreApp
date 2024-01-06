namespace Infrastructure.Persistence.EntityConfigurations.BaseConfigurations;

public class BaseEntityConfiguration<TBase> : IEntityTypeConfiguration<TBase> where TBase : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<TBase> entityTypeBuilder)
    {

        entityTypeBuilder.Property(x => x.Id).HasDefaultValueSql("NEWID()").IsRequired();
        entityTypeBuilder.Property(x => x.CreatedById).IsRequired();
        entityTypeBuilder.Property(x => x.CreatedDate).HasDefaultValueSql("GETDATE()").IsRequired();

    }
}