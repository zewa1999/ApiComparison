using ApiComparison.Application.Interfaces.BusinessServices;
using ApiComparison.Contracts.RequestDto;
using ApiComparison.Contracts.ResponseDto;
using ApiComparison.Domain.Entities;
using ApiComparison.Mapping.Base;

namespace ApiComparison.GraphQLApi.CommandsQueries;

public class Query
{
    [UseFiltering]
    [UseSorting]
    public async Task<IEnumerable<AccountResponseDto>> GetAccount(
        [ScopedService] IAccountService accountService,
        [ScopedService] IMapper<Account, AccountRequestDto, AccountResponseDto> mapper
        )
    {
        var accounts = await accountService.GetAllAsync(CancellationToken.None);
        return accounts.Select(mapper.EntityToResponse);
    }

    [UseFiltering]
    [UseSorting]
    public async Task<IEnumerable<AddressResponseDto>> GetAddresses(
    [ScopedService] IAddressService addressService,
    [ScopedService] IMapper<Address, AddressRequestDto, AddressResponseDto> mapper)
    {
        var addresses = await addressService.GetAllAsync(CancellationToken.None);
        return addresses.Select(mapper.EntityToResponse);
    }

    [UseFiltering]
    [UseSorting]
    public async Task<IEnumerable<DishResponseDto>> GetDishes(
    [ScopedService] IDishService dishService,
    [ScopedService] IMapper<Dish, DishRequestDto, DishResponseDto> mapper)
    {
        var dishes = await dishService.GetAllAsync(CancellationToken.None);
        return dishes.Select(mapper.EntityToResponse);
    }

    [UseFiltering]
    [UseSorting]
    public async Task<IEnumerable<IngredientResponseDto>> GetIngredients(
    [ScopedService] IIngredientService ingredientService,
    [ScopedService] IMapper<Ingredient, IngredientRequestDto, IngredientResponseDto> mapper)
    {
        var ingredients = await ingredientService.GetAllAsync(CancellationToken.None);
        return ingredients.Select(mapper.EntityToResponse);
    }

    [UseFiltering]
    [UseSorting]
    public async Task<IEnumerable<UserResponseDto>> GetUsers(
    [ScopedService] IUserService userService,
    [ScopedService] IMapper<User, UserRequestDto, UserResponseDto> mapper)
    {
        var users = await userService.GetAllAsync(CancellationToken.None);
        return users.Select(mapper.EntityToResponse);
    }
}