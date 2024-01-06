using Infrastructure.Common.Interfaces;
using Infrastructure.Common.Repositories;
using Infrastructure.Persistence.Context;

namespace Infrastructure.Repositories;

public class StudentRepository : BaseRepository<Student>, IStudentRepository
{
    public readonly NgCoreAppContext _context;
    public StudentRepository(NgCoreAppContext context) : base(context)
    {
        _context = context;
    }
}
