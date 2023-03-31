using ApiComparison.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ApiComparison.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection serviceCollection)
    {
        return serviceCollection;
    }
}
