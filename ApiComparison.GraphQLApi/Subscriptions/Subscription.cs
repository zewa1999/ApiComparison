using ApiComparison.Domain.Entities;

namespace ApiComparison.GraphQLApi.Subscriptions;

public class Subscription
{
    [Subscribe]
    [Topic]
    public Account OnAccountAdded([EventMessage] Account account) => account;

    [Subscribe]
    [Topic]
    public Address OnAddressAdded([EventMessage] Address address) => address;

    [Subscribe]
    [Topic]
    public Dish OnDishAdded([EventMessage] Dish dish) => dish;

    [Subscribe]
    [Topic]
    public Ingredient OnIngredientAdded([EventMessage] Ingredient ingredient) => ingredient;

    [Subscribe]
    [Topic]
    public User OnUserAdded([EventMessage] User user) => user;
}