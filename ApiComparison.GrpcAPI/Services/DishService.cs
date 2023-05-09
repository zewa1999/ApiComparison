using ApiComparison.Application.Interfaces;
using ApiComparison.Domain.Entities;
using ApiComparison.GrpcAPI;
using ApiComparison.Infrastructure.BusinessLogicServices;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System.Linq;
using Dish = ApiComparison.GrpcAPI.Dish;

namespace ApiComparison.GrpcApi.Services;

public class DishService : Dish.DishBase
{
    private readonly IDishService _dishService;
    private readonly IIngredientService _ingredientService;

    public DishService(IDishService dishService, IIngredientService ingredientService)
    {
        _dishService = dishService;
        _ingredientService = ingredientService;
    }

    public override async Task<DishResponseDto> GetDish(Id request, ServerCallContext context)
    {
        Domain.Entities.Dish dish = null!;

        if (!string.IsNullOrEmpty(request.Id_))
        {
            Guid.TryParse(request.Id_, out var addressId);
            dish = await _dishService.GetByIdAsync(addressId, context.CancellationToken);
        }

        var response = new DishResponseDto()
        {
            Id = request.Id_,
            Description = dish.Description,
            Name = dish.Name,
            PhotoUrl = dish.PhotoUrl
        };

        response.Ingredients.AddRange(dish.DishIngredients.Select((Domain.Entities.Ingredient ingredient) => new IngredientResponseDto
        {
            Name = ingredient.Name,
            Quantity = ingredient.Quantity,
            UnitOfMeasure = ingredient.UnitOfMeasure
        }));

        return response;
    }

    public override async Task<DishListResponseDto> GetDishes(DishRequestDto request, ServerCallContext context)
    {
        var dishes = await _dishService.GetAllAsync(context.CancellationToken);

        return new DishListResponseDto
        {
            Items = { dishes.Select((Domain.Entities.Dish dish) => new DishResponseDto
            {
                 Id = dish.Id.ToString(),
                 Description = dish.Description,
                 Name = dish.Name,
                 PhotoUrl = dish.PhotoUrl,
                Ingredients =
                { dish.DishIngredients.Select((Domain.Entities.Ingredient ingredient) => new IngredientResponseDto
                {
                     Id = ingredient.Id.ToString(),
                     Quantity = ingredient.Quantity,
                     Name = dish.Name,
                     UnitOfMeasure = ingredient.UnitOfMeasure
                })}
            })
            }
        };
    }

    public override async Task<DishResponseDto> PostDish(DishRequestDto request, ServerCallContext context)
    {
        // ugly linq thing
        var dishIngredients = await Task.WhenAll(request.IngredientIds.Select(async x => await _ingredientService.GetByIdAsync(Guid.Parse(x), context.CancellationToken)));

        var dish = await _dishService.InsertAsync(new Domain.Entities.Dish
        {
            Description = request.Description,
            Name = request.Name,
            PhotoUrl = request.PhotoUrl,
            DishIngredients = dishIngredients
        }, context.CancellationToken);

        return new DishResponseDto
        {
            Id = dish.Id.ToString(),
            Description = dish.Description,
            Name = dish.Name,
            PhotoUrl = dish.PhotoUrl,
            Ingredients =
                { dish.DishIngredients.Select((Domain.Entities.Ingredient ingredient) => new IngredientResponseDto
                {
                     Id = ingredient.Id.ToString(),
                     Quantity = ingredient.Quantity,
                     Name = dish.Name,
                     UnitOfMeasure = ingredient.UnitOfMeasure
                })}
        };
    }

    public override async Task<Empty> PutDish(DishPutRequestDto request, ServerCallContext context)
    {
        var dishIngredients = await Task.WhenAll(request.IngredientIds.Select(async x => await _ingredientService.GetByIdAsync(Guid.Parse(x), context.CancellationToken)));

        if (!string.IsNullOrEmpty(request.Id.Id_))
        {
            Guid.TryParse(request.Id.Id_, out var dishId);
            await _dishService.UpdateAsync(dishId, new Domain.Entities.Dish
            {
                Description = request.Description,
                Name = request.Name,
                PhotoUrl = request.PhotoUrl,
                DishIngredients = dishIngredients,
            }, context.CancellationToken);
        }

        return new Empty();
    }

    public override async Task<Empty> DeleteDish(Id request, ServerCallContext context)
    {
        if (!string.IsNullOrEmpty(request.Id_))
        {
            Guid.TryParse(request.Id_, out var dishId);
            await _dishService.DeleteByIdAsync(dishId, context.CancellationToken);
        }

        return new Empty();
    }
}