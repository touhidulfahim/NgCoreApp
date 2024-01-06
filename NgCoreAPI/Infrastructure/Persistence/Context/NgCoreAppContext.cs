using Infrastructure.Common.Interfaces;
using System.Reflection;

namespace Infrastructure.Persistence.Context;

public partial class NgCoreAppContext : DbContext, INgCoreAppContext
{
    public NgCoreAppContext()
    {
    }

    public NgCoreAppContext(DbContextOptions<NgCoreAppContext> options)
        : base(options)
    {
    }

    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Student> Students => Set<Student>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
