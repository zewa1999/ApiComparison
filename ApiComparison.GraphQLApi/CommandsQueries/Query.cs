using ApiComparison.Domain.Entities;
using ApiComparison.EfCore.Persistence;

namespace ApiComparison.GraphQLApi.CommandsQueries;

public class Query
{
    [UseDbContext(typeof(ApiComparisonDbContext))]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Account> GetAccount(
        [ScopedService] ApiComparisonDbContext context)
    {
        return context.Accounts;
    }

    [UseDbContext(typeof(ApiComparisonDbContext))]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Address> GetAddresses(
         [ScopedService] ApiComparisonDbContext context)
    {
        return context.Addresses;
    }

    [UseDbContext(typeof(ApiComparisonDbContext))]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Dish> GetDishes(
    [ScopedService] ApiComparisonDbContext context)
    {
        var dishes = context.Dishes;

        return dishes;
    }

    [UseDbContext(typeof(ApiComparisonDbContext))]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Ingredient> GetIngredients(
    [ScopedService] ApiComparisonDbContext context)
    {
        return context.Ingredients;
    }

    [UseDbContext(typeof(ApiComparisonDbContext))]
    [UseFiltering]
    [UseSorting]
    public IEnumerable<User> GetUsers(
    [ScopedService] ApiComparisonDbContext context)
    {
        return context.Users;
    }
}