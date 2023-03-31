using ApiComparison.Domain.Interfaces.Repositories;
using ApiComparison.EfCore.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ApiComparison.EfCore.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistenceLayer(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IAccountRepository, AccountRepository>();
        serviceCollection.AddScoped<IAddressRepository, AddressRepository>();
        serviceCollection.AddScoped<IDishRepository, DishRepository>();
        serviceCollection.AddScoped<IIngredientRepository, IngredientRepository>();
        serviceCollection.AddScoped<IUserRepository, UserRepository>();

        return serviceCollection;
    }
}
