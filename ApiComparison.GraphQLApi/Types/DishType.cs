using ApiComparison.Application.Interfaces.BusinessServices;
using ApiComparison.Domain.Entities;
using ApiComparison.EfCore.Persistence;
using Microsoft.EntityFrameworkCore;
using System;

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

        descriptor.Field(d => d.Ingredients)
            .Description("The list of ingredients used in the dish.")
            .Type<ListType<IngredientType>>()
            .ResolveWith<DishResolvers>(r => r.GetIngredients(default!, default!));
    }

    private class DishResolvers
    {
        [UseDbContext(typeof(ApiComparisonDbContext))]
        public IEnumerable<Ingredient> GetIngredients([Parent] Dish dish, [ScopedService] ApiComparisonDbContext context)
        {
            var ingredients = context.Dishes
                .Include(dish => dish.Ingredients)
                .Where(dish => dish.Id == dish.Id)
                .FirstOrDefault()!
                .Ingredients;

            ArgumentNullException.ThrowIfNull(ingredients);

            return ingredients;
        }
    }
}