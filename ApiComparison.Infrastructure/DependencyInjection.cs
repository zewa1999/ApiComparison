using ApiComparison.Application.Interfaces.BusinessServices;
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
        serviceCollection.AddScoped<IAccountService, AccountService>();
        serviceCollection.AddScoped<IAddressService, AddressService>();
        serviceCollection.AddScoped<IDishService, DishService>();
        serviceCollection.AddScoped<IIngredientService, IngredientService>();
        serviceCollection.AddScoped<IUserService, UserService>();

        return serviceCollection;
    }
}