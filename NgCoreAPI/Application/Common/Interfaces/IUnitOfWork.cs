namespace Infrastructure.Common.Interfaces;

public interface IUnitOfWork
{
    public IDepartmentRepository DepartmentRepository { get; }
    public IStudentRepository StudentRepository { get; }

    public int Commit();
    public Task<int> CommitAsync();
}
