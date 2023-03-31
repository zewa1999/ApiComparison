using ApiComparison.Contracts.Dto;
using ApiComparison.Contracts.Dto.RequestDto;
using ApiComparison.Contracts.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace ApiComparison.Contracts;

public static class DependencyInjection
{
    public static IServiceCollection AddValidationLayer(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IValidator<BaseDto>, BaseDtoValidator>();
        serviceCollection.AddScoped<IValidator<AccountRequestDto>, AccountRequestDtoValidator>();
        serviceCollection.AddScoped<IValidator<AddressRequestDto>, AddressRequestDtoValidator>();
        serviceCollection.AddScoped<IValidator<DishRequestDto>, DishRequestDtoValidator>();
        serviceCollection.AddScoped<IValidator<IngredientRequestDto>, IngredientRequestDtoValidator>();
        serviceCollection.AddScoped<IValidator<UserRequestDto>, UserRequestDtoValidator>();

        return serviceCollection;
    }
}
