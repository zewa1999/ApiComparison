using ApiComparison.Domain.Entities;
using ApiComparison.EfCore.Persistence;

namespace ApiComparison.GraphQLApi.Types;

public class IngredientType : ObjectType<Ingredient>
{
    protected override void Configure(IObjectTypeDescriptor<Ingredient> descriptor)
    {
        descriptor.Description("Represents an ingredient.");

        descriptor.Field(i => i.Id)
            .Description("The unique identifier of the ingredient.");

        descriptor.Field(i => i.Name)
            .Description("The name of the ingredient.");

        descriptor.Field(i => i.Quantity)
            .Description("The quantity of the ingredient.");

        descriptor.Field(i => i.UnitOfMeasure)
            .Description("The unit of measure for the ingredient.");

        descriptor.Field(i => i.Dishes)
            .Description("The list of dishes in which the ingredient is used.")
            .Type<ListType<DishType>>()
            .ResolveWith<IngredientResolvers>(i => i.GetDishes(default!, default!));
    }

    private class IngredientResolvers
    {
        [UseDbContext(typeof(ApiComparisonDbContext))]
        public IEnumerable<Dish> GetDishes([Parent] Ingredient ingredient, [ScopedService] ApiComparisonDbContext context)
        {
            var dishes = context.Ingredients
                .Where(ingredient => ingredient.Id == ingredient.Id)
                .FirstOrDefault()!
                .Dishes;

            ArgumentNullException.ThrowIfNull(dishes);

            return dishes;
        }
    }
}