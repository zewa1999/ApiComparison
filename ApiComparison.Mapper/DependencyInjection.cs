using ApiComparison.Contracts.Dto.RequestDto;
using ApiComparison.Contracts.Dto.ResponseDto;
using ApiComparison.Domain.Entities;
using ApiComparison.Mapping.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace ApiComparison.Mapping;

public static class DependencyInjection
{
    public static IServiceCollection AddMappingLayer(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IBaseMapper<Account, AccountRequestDto, AccountResponseDto>, AccountMapper>();
        serviceCollection.AddScoped<IBaseMapper<Address, AddressRequestDto, AddressResponseDto>, AddressMapper>();
        serviceCollection.AddScoped<IBaseMapper<Dish, DishRequestDto, DishResponseDto>, DishMapper>();
        serviceCollection.AddScoped<IBaseMapper<Ingredient, IngredientRequestDto, IngredientResponseDto>, IngredientMapper>();
        serviceCollection.AddScoped<IBaseMapper<User, UserRequestDto, UserResponseDto>, UserMapper>();

        return serviceCollection;
    }
}
