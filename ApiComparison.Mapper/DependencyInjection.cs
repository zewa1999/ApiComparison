using ApiComparison.Contracts.RequestDto;
using ApiComparison.Contracts.ResponseDto;
using ApiComparison.Domain.Entities;
using ApiComparison.Mapping.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace ApiComparison.Mapping;

public static class DependencyInjection
{
    public static IServiceCollection AddMappingLayer(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IMapper<Account, AccountRequestDto, AccountResponseDto>, AccountMapper>();
        serviceCollection.AddScoped<IMapper<User, UserRequestDto, UserResponseDto>, AddressUserMapper>();
        serviceCollection.AddScoped<IMapper<Address, AddressRequestDto, AddressResponseDto>, AddressUserMapper>();
        serviceCollection.AddScoped<IMapper<Dish, DishRequestDto, DishResponseDto>, DishIngredientMapper>();
        serviceCollection.AddScoped<IMapper<Ingredient, IngredientRequestDto, IngredientResponseDto>, DishIngredientMapper>();

        return serviceCollection;
    }
}