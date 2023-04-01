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
        serviceCollection.AddDbContext<ApiComparisonDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("Pgsql Connection String"));
        });

        serviceCollection.AddScoped<IAccountRepository, AccountRepository>();
        serviceCollection.AddScoped<IAddressRepository, AddressRepository>();
        serviceCollection.AddScoped<IDishRepository, DishRepository>();
        serviceCollection.AddScoped<IIngredientRepository, IngredientRepository>();
        serviceCollection.AddScoped<IUserRepository, UserRepository>();

        return serviceCollection;
    }
}
