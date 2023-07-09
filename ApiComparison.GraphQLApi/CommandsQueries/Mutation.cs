using ApiComparison.Contracts.UserDtos;
using ApiComparison.Domain.Entities;
using ApiComparison.EfCore.Persistence;
using ApiComparison.GraphQLApi.Subscriptions;
using ApiComparison.Validation.Extensions;
using FluentValidation;
using HotChocolate.Subscriptions;

namespace ApiComparison.GraphQLApi.CommandsQueries;

public class Mutation
{
    [UseDbContext(typeof(ApiComparisonDbContext))]
    public async Task<Account> AddAccountAsync(Account account,
        [ScopedService] ApiComparisonDbContext context,
        [ScopedService] IValidator<Account> validator,
        [Service] ITopicEventSender eventSender,
        CancellationToken cancellationToken)
    {
        validator.ValidateAndThrowAggregateException(account);

        var dbAccount = await context.Accounts.AddAsync(account, cancellationToken);

        await eventSender.SendAsync(nameof(Subscription.OnAccountAdded), account, cancellationToken);

        return dbAccount.Entity;
    }

    [UseDbContext(typeof(ApiComparisonDbContext))]
    public async Task<Address> AddAddressAsync(Address address,
        [ScopedService] ApiComparisonDbContext context,
        [ScopedService] IValidator<Address> validator,
        [Service] ITopicEventSender eventSender,
        CancellationToken cancellationToken)
    {
        validator.ValidateAndThrowAggregateException(address);

        var dbAddress = await context.Addresses.AddAsync(address, cancellationToken);

        await eventSender.SendAsync(nameof(Subscription.OnAddressAdded), address, cancellationToken);

        return dbAddress.Entity;
    }

    [UseDbContext(typeof(ApiComparisonDbContext))]
    public async Task<Dish> AddDishAsync(Dish dish,
        [ScopedService] ApiComparisonDbContext context,
        [ScopedService] IValidator<Dish> validator,
        [Service] ITopicEventSender eventSender,
        CancellationToken cancellationToken)
    {
        validator.ValidateAndThrowAggregateException(dish);

        var dbDish = await context.Dishes.AddAsync(dish, cancellationToken);

        await eventSender.SendAsync(nameof(Subscription.OnDishAdded), dish, cancellationToken);

        return dbDish.Entity;
    }

    [UseDbContext(typeof(ApiComparisonDbContext))]
    public async Task<Ingredient> AddIngredientAsync(Ingredient ingredient,
        [ScopedService] ApiComparisonDbContext context,
        [ScopedService] IValidator<Ingredient> validator,
        [Service] ITopicEventSender eventSender,
        CancellationToken cancellationToken)
    {

        validator.ValidateAndThrowAggregateException(ingredient);

        var dbIngredient = await context.Ingredients.AddAsync(ingredient, cancellationToken);

        await eventSender.SendAsync(nameof(Subscription.OnIngredientAdded), ingredient, cancellationToken);

        return dbIngredient.Entity;
    }

    [UseDbContext(typeof(ApiComparisonDbContext))]
    public async Task<User> AddUserAsync(UserCreateRequestDto requestDto,
        [ScopedService] ApiComparisonDbContext context,
        [ScopedService] IValidator<User> validator,
        [Service] ITopicEventSender eventSender,
        CancellationToken cancellationToken)
    {
        var userEntity =  new User
            {
                FirstName = requestDto.FirstName,
                LastName = requestDto.LastName,
                Bio = requestDto.Bio,
                Account = new Account
                {
                    Email = requestDto.Email,
                    Password = requestDto.Password,
                    Username = requestDto.Username
                },
                Address = new Address
                {
                    City = requestDto.City,
                    Street = requestDto.Street,
                    StreetNumber = requestDto.StreetNumber,
                }
            };

        validator.ValidateAndThrowAggregateException(userEntity);
        var dbUser = await context.AddAsync(userEntity, cancellationToken);

        await eventSender.SendAsync(nameof(Subscription.OnUserAdded), requestDto, cancellationToken);

        return dbUser.Entity;
    }
}