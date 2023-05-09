using ApiComparison.Application.Interfaces;
using ApiComparison.Domain.Entities;

namespace ApiComparison.GraphQLApi.Types;

public class DishType : ObjectType<Dish>
{
    protected override void Configure(IObjectTypeDescriptor<Dish> descriptor)
    {
        descriptor.Description("Represents a dish.");

        descriptor.Field(d => d.Id)
            .Description("The unique identifier of the dish.");

        descriptor.Field(d => d.Name)
            .Description("The name of the dish.");

        descriptor.Field(d => d.Description)
            .Description("The description of the dish.");

        descriptor.Field(d => d.PhotoUrl)
            .Description("The URL of the dish's photo.");

        descriptor.Field(d => d.DishIngredients)
            .Description("The list of ingredients used in the dish.")
            .Type<ListType<IngredientType>>()
            .ResolveWith<DishResolvers>(r => r.GetIngredients(default!, default!, CancellationToken.None));
    }

    private class DishResolvers
    {
        public async Task<IEnumerable<Ingredient>> GetIngredients(Dish dish, [ScopedService] IIngredientService ingredientService, CancellationToken cancellationToken)
        {
            var ingredients = await ingredientService.GetAllAsync(cancellationToken);
            ArgumentNullException.ThrowIfNull(ingredients);
            if (ingredients.Any())
            {
                throw new InvalidOperationException("The dish with id ");
            }

            return ingredients;
        }
    }
}