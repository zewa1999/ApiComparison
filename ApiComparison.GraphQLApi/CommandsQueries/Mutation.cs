using ApiComparison.Application.Interfaces;
using ApiComparison.Contracts.RequestDto;
using ApiComparison.Contracts.ResponseDto;
using ApiComparison.Domain.Entities;
using ApiComparison.GraphQLApi.Subscriptions;
using ApiComparison.Mapping.Base;
using HotChocolate.Subscriptions;

namespace ApiComparison.GraphQLApi.CommandsQueries;

public class Mutation
{
    public async Task<AccountResponseDto> AddPlatformAsync(AccountRequestDto requestDto,
        [ScopedService] IAccountService service,
        [ScopedService] IMapper<Account, AccountRequestDto, AccountResponseDto> mapper,
        [Service] ITopicEventSender eventSender,
        CancellationToken cancellationToken)
    {
        var account = mapper.RequestToEntity(requestDto);

        var dbAccount = await service.InsertAsync(account, cancellationToken);

        await eventSender.SendAsync(nameof(Subscription.OnAccountAdded), account, cancellationToken);

        return mapper.EntityToResponse(dbAccount);
    }

    public async Task<AddressResponseDto> AddAddressAsync(AddressRequestDto requestDto,
        [ScopedService] IAddressService addressService,
        [ScopedService] IMapper<Address, AddressRequestDto, AddressResponseDto> mapper,
        [Service] ITopicEventSender eventSender,
        CancellationToken cancellationToken)
    {
        var address = mapper.RequestToEntity(requestDto);

        var dbAddress = await addressService.InsertAsync(address, cancellationToken);

        await eventSender.SendAsync(nameof(Subscription.OnAddressAdded), address, cancellationToken);

        return mapper.EntityToResponse(dbAddress);
    }

    public async Task<DishResponseDto> AddDishAsync(DishRequestDto requestDto,
        [ScopedService] IDishService dishService,
        [ScopedService] IMapper<Dish, DishRequestDto, DishResponseDto> mapper,
        [Service] ITopicEventSender eventSender,
        CancellationToken cancellationToken)
    {
        var dish = mapper.RequestToEntity(requestDto);

        var dbDish = await dishService.InsertAsync(dish, cancellationToken);

        await eventSender.SendAsync(nameof(Subscription.OnDishAdded), dish, cancellationToken);

        return mapper.EntityToResponse(dbDish);
    }

    public async Task<IngredientResponseDto> AddIngredientAsync(IngredientRequestDto requestDto,
        [ScopedService] IIngredientService ingredientService,
        [ScopedService] IMapper<Ingredient, IngredientRequestDto, IngredientResponseDto> mapper,
        [Service] ITopicEventSender eventSender,
        CancellationToken cancellationToken)
    {
        var ingredient = mapper.RequestToEntity(requestDto);

        var dbIngredient = await ingredientService.InsertAsync(ingredient, cancellationToken);

        await eventSender.SendAsync(nameof(Subscription.OnIngredientAdded), ingredient, cancellationToken);

        return mapper.EntityToResponse(dbIngredient);
    }

    public async Task<UserResponseDto> AddUserAsync(UserRequestDto requestDto,
        [ScopedService] IUserService userService,
        [ScopedService] IMapper<User, UserRequestDto, UserResponseDto> mapper,
        [Service] ITopicEventSender eventSender,
        CancellationToken cancellationToken)
    {
        var user = mapper.RequestToEntity(requestDto);

        var dbUser = await userService.InsertAsync(user, cancellationToken);

        await eventSender.SendAsync(nameof(Subscription.OnUserAdded), user, cancellationToken);

        return mapper.EntityToResponse(dbUser);
    }
}