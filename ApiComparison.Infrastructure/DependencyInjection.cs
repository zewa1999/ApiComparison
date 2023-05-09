using ApiComparison.Application.Interfaces;
using ApiComparison.EfCore.Persistence;
using ApiComparison.Infrastructure.BusinessLogicServices;
using ApiComparison.Validation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiComparison.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddValidationLayer();
        serviceCollection.AddPersistenceLayer(configuration);
        serviceCollection.AddServiceLayer();

        return serviceCollection;
    }

    public static IServiceCollection AddServiceLayer(this IServiceCollection serviceCollection)
    {
        // these are transient because the dbcontext can throw errors when multiple queries are run in parallel using the graphql presentation layer
        serviceCollection.AddTransient<IAccountService, AccountService>();
        serviceCollection.AddTransient<IAddressService, AddressService>();
        serviceCollection.AddTransient<IDishService, DishService>();
        serviceCollection.AddTransient<IIngredientService, IngredientService>();
        serviceCollection.AddTransient<IUserService, UserService>();

        return serviceCollection;
    }
}