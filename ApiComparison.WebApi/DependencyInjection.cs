using ApiComparison.Application;
using ApiComparison.Infrastructure;

namespace ApiComparison.WebApi;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddApplicationLayer();
        serviceCollection.AddInfrastructureLayer(configuration);

        return serviceCollection;
    }
}