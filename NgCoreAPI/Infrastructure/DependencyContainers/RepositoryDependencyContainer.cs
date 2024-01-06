namespace Infrastructure.DependencyContainers;

public class RepositoryDependencyContainer
{

    public static void RegisterServices(IServiceCollection services)
    {
        services.AddScoped<IDepartmentRepository, DepartmentRepository>();
        services.AddScoped<IStudentRepository, StudentRepository>();
    }
}
