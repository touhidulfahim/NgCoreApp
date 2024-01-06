using Infrastructure.Persistence.Context;

namespace Infrastructure.UOW;

public class UnitOfWork : IDisposable, IUnitOfWork
{
    private NgCoreAppContext _context;
    public UnitOfWork(NgCoreAppContext context)
    {
        _context = context;
    }
    public IDepartmentRepository _departmentRepository;
    public IDepartmentRepository DepartmentRepository
    {
        get
        {
            if (this._departmentRepository == null)
            {
                this._departmentRepository = new DepartmentRepository(_context);
            }

            return _departmentRepository;
        }
    }
    public IStudentRepository _studentRepository;
    public IStudentRepository StudentRepository
    {
        get
        {
            if (this._studentRepository == null)
            {
                this._studentRepository = new StudentRepository(_context);
            }

            return _studentRepository;
        }
    }
    public int Commit()
    {
        return _context.SaveChanges();
    }
    public async Task<int> CommitAsync()
    {
        return await _context.SaveChangesAsync();
    }
    #region dispose
    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        this.disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    #endregion
}
