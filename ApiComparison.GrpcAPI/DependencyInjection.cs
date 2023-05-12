using ApiComparison.Application.Interfaces.BusinessServices;
using ApiComparison.Infrastructure.BusinessLogicServices;

namespace ApiComparison.GrpcApi;

public static class DependencyInjection
{
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