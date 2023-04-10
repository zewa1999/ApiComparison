using ApiComparison.Contracts.Validators;
using ApiComparison.Domain.Entities;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace ApiComparison.Validation;

public static class DependencyInjection
{
    public static IServiceCollection AddValidationLayer(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IValidator<Account>, AccountValidator>();
        serviceCollection.AddScoped<IValidator<Address>, AddressValidator>();
        serviceCollection.AddScoped<IValidator<Dish>, DishValidator>();
        serviceCollection.AddScoped<IValidator<Ingredient>, IngredientValidator>();
        serviceCollection.AddScoped<IValidator<User>, UserValidator>();

        return serviceCollection;
    }
}