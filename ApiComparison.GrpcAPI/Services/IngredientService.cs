using ApiComparison.Application.Interfaces;
using ApiComparison.Domain.Entities;
using ApiComparison.GrpcAPI;
using ApiComparison.Infrastructure.BusinessLogicServices;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System.Linq;
using System.Runtime.CompilerServices;
using Ingredient = ApiComparison.GrpcAPI.Ingredient;

namespace ApiComparison.GrpcApi.Services;

public class IngredientService : Ingredient.IngredientBase
{
    private readonly IIngredientService _ingredientService;
    private readonly IDishService _dishService;

    public IngredientService(IIngredientService ingredientService, IDishService dishService)
    {
        _ingredientService = ingredientService;
        _dishService = dishService;
    }

    public override async Task<IngredientResponseDto> GetIngredient(Id request, ServerCallContext context)
    {
        Domain.Entities.Ingredient ingredient = null!;

        if (!string.IsNullOrEmpty(request.Id_))
        {
            Guid.TryParse(request.Id_, out var ingredientId);
            ingredient = await _ingredientService.GetByID(ingredientId, context.CancellationToken);
        }

        var response = new IngredientResponseDto
        {
            Id = ingredient.Id.ToString(),
            Name = ingredient.Name,
            Quantity = ingredient.Quantity,
            UnitOfMeasure = ingredient.UnitOfMeasure
        };

        response.Dishes.AddRange(ingredient.DishIngredients.Select((Domain.Entities.Dish dish) => new DishResponseDto
        {
            Description = dish.Description,
            Id = dish.Id.ToString(),
            Name = dish.Name,
            PhotoUrl = dish.PhotoUrl,
        }));

        return response;
    }

    public override async Task<IngredientListResponseDto> GetIngredients(IngredientRequestDto request, ServerCallContext context)
    {
        var ingredients = await _ingredientService.GetAll(context.CancellationToken);

        return new IngredientListResponseDto
        {
            Items = { ingredients.Select((Domain.Entities.Ingredient ingredient) => new IngredientResponseDto
            {
                Id = ingredient.Id.ToString(),
                Name = ingredient.Name,
                Quantity = ingredient.Quantity,
                UnitOfMeasure = ingredient.UnitOfMeasure,
                Dishes =
                { ingredient.DishIngredients.Select((Domain.Entities.Dish dish) => new DishResponseDto
                {
                     Description = dish.Description,
                     Id = dish.Id.ToString(),
                     Name = dish.Name,
                     PhotoUrl = dish.PhotoUrl,
                })}
            })
            }
        };
    }

    public override async Task<IngredientResponseDto> PostIngredient(IngredientRequestDto request, ServerCallContext context)
    {
        // ugly linq thing
        var ingredientDishes = await Task.WhenAll(request.DishIds.Select(async x => await _dishService.GetByID(Guid.Parse(x), context.CancellationToken)));

        var ingredient = await _ingredientService.Insert(new Domain.Entities.Ingredient
        {
            Name = request.Name,
            Quantity = request.Quantity,
            UnitOfMeasure = request.UnitOfMeasure,
            DishIngredients = ingredientDishes
        }, context.CancellationToken);

        return new IngredientResponseDto
        {
            Id = ingredient.Id.ToString(),
            Name = ingredient.Name,
            Quantity = ingredient.Quantity,
            UnitOfMeasure = ingredient.UnitOfMeasure,
            Dishes =
                { ingredient.DishIngredients.Select((Domain.Entities.Dish dish) => new DishResponseDto
                {
                     Description = dish.Description,
                     Id = dish.Id.ToString(),
                     Name = dish.Name,
                     PhotoUrl = dish.PhotoUrl,
                })}
        };
    }

    public override async Task<Empty> PutIngredient(IngredientPutRequestDto request, ServerCallContext context)
    {
        var ingredientDishes = await Task.WhenAll(request.DishIds.Select(async x => await _dishService.GetByID(Guid.Parse(x), context.CancellationToken)));

        if (!string.IsNullOrEmpty(request.Id.Id_))
        {
            Guid.TryParse(request.Id.Id_, out var ingredientId);
            await _ingredientService.Update(ingredientId, new Domain.Entities.Ingredient
            {
                Name = request.Name,
                Quantity = request.Quantity,
                UnitOfMeasure = request.UnitOfMeasure,
                DishIngredients = ingredientDishes
            }, context.CancellationToken);
        }

        return new Empty();
    }

    public override async Task<Empty> DeleteIngredient(Id request, ServerCallContext context)
    {
        if (!string.IsNullOrEmpty(request.Id_))
        {
            Guid.TryParse(request.Id_, out var ingredientId);
            await _ingredientService.DeleteById(ingredientId, context.CancellationToken);
        }

        return new Empty();
    }
}