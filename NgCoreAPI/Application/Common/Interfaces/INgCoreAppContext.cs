using Domain.Entities;

namespace Infrastructure.Common.Interfaces;

public interface INgCoreAppContext
{
    DbSet<Department> Departments { get; }
    DbSet<Student> Students { get; }
}
