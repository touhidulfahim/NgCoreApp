using Infrastructure.Common.Repositories;
using Infrastructure.Persistence.Context;

namespace Infrastructure.Repositories;

public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
{
    public readonly NgCoreAppContext _context;
    public DepartmentRepository(NgCoreAppContext context) : base(context)
    {
        _context = context;
    }
}
