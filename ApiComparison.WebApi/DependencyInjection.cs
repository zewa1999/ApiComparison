using ApiComparison.Application;
using ApiComparison.Infrastructure;
using ApiComparison.Mapping;

namespace ApiComparison.WebApi;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddInfrastructureLayer(configuration);
        serviceCollection.AddMappingLayer();

        return serviceCollection;
    }
}