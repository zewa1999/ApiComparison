using ApiComparison.Mapping.Base;
using ApiComparison.Mapping.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace ApiComparison.Mapping;

public static class DependencyInjection
{
    public static IServiceCollection AddMappingLayer(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IAccountMapper, AccountMapper>();
        serviceCollection.AddScoped<IUserMapper, UserMapper>();
        serviceCollection.AddScoped<IAddressMapper, AddressMapper>();
        serviceCollection.AddScoped<IDishMapper, DishMapper>();
        serviceCollection.AddScoped<IIngredientMapper, IngredientMapper>();

        return serviceCollection;
    }
}