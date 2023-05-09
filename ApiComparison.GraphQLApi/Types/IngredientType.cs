using ApiComparison.Application.Interfaces;
using ApiComparison.Domain.Entities;

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

        descriptor.Field(i => i.DishIngredients)
            .Description("The list of dishes in which the ingredient is used.")
            .Type<ListType<DishType>>();
    }

    private class IngredientResolvers
    {
        public async Task<IEnumerable<Dish>> GetIngredients([Parent] Ingredient ingredient, [ScopedService] IDishService dishService, CancellationToken cancellationToken)
        {
            var dishes = await dishService.GetAllAsync(cancellationToken);
            ArgumentNullException.ThrowIfNull(dishes);
            if (dishes.Any())
            {
                throw new InvalidOperationException("The dish with id ");
            }

            return dishes;
        }
    }
}