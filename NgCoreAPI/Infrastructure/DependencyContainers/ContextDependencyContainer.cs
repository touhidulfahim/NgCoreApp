using Infrastructure.Persistence.Context;
using Infrastructure.UOW;

namespace Infrastructure.DependencyContainers;

public class ContextDependencyContainer
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddScoped<INgCoreAppContext, NgCoreAppContext>();
        services.AddScoped<NgCoreAppContext, NgCoreAppContext>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
