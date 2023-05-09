using ApiComparison.Domain.Interfaces.Repositories;
using ApiComparison.EfCore.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiComparison.EfCore.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistenceLayer(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddPooledDbContextFactory<ApiComparisonDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("Pgsql Connection String"));
        });
        serviceCollection.AddTransient<IAccountRepository, AccountRepository>();
        serviceCollection.AddTransient<IAddressRepository, AddressRepository>();
        serviceCollection.AddTransient<IDishRepository, DishRepository>();
        serviceCollection.AddTransient<IIngredientRepository, IngredientRepository>();
        serviceCollection.AddTransient<IUserRepository, UserRepository>();

        return serviceCollection;
    }
}