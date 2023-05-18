using ApiComparison.Application.Interfaces.BusinessServices;
using ApiComparison.Domain.Entities;
using ApiComparison.GrpcAPI;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
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
            Guid.TryParse(request.Id_, out var dishId);
            dish = await _dishService.GetByIdAsync(dishId, context.CancellationToken);
        }

        var response = new DishResponseDto()
        {
            Id = request,
            Description = dish.Description,
            Name = dish.Name,
            PhotoUrl = dish.PhotoUrl
        };

        return response;
    }

    public override async Task<DishListResponseDto> GetDishes(DishRequestDto request, ServerCallContext context)
    {
        var dishes = await _dishService.GetAllAsync(context.CancellationToken);

        var response = new DishListResponseDto();


        foreach (var dish in dishes)
        {
            Id dishId = new Id();
            dishId.Id_ = dish.Id.ToString();

            response.Items.Add(new DishResponseDto
            {
                Id = dishId,
                Name = dish.Name,
                Description = dish.Description,
                PhotoUrl = dish.PhotoUrl
            });
        }
        return response;
    }
    public override async Task<IngredientListResponseDto> GetDishIngredients(Id request, ServerCallContext context)
    {
        Guid.TryParse(request.Id_, out var dishId);

        var ingredients = await _dishService.GetIngredientsOfDish(dishId, context.CancellationToken);

        var response = new IngredientListResponseDto();

        foreach (var ingredient in ingredients)
        {
            Id ingredientId = new Id();
            ingredientId.Id_ = ingredient.Id.ToString();

            response.Items.Add(new IngredientResponseDto
            {
                Id = ingredientId,
                Name = ingredient.Name,
                Quantity = ingredient.Quantity,
                UnitOfMeasure = ingredient.UnitOfMeasure,
            });
        }

        return response;
    }
    public override async Task<DishResponseDto> PostDish(DishRequestDto request, ServerCallContext context)
    {
        // ugly linq thing
        var ingredientsOfDish = await Task.WhenAll(request.IngredientIds.Select(async x => await _ingredientService.GetByIdAsync(Guid.Parse(x), context.CancellationToken)));

        var dish = await _dishService.InsertAsync(new Domain.Entities.Dish
        {
            Description = request.Description,
            Name = request.Name,
            PhotoUrl = request.PhotoUrl,
            Ingredients = ingredientsOfDish
        }, context.CancellationToken);

        Id dishId = new Id();
        dishId.Id_ = dish.Id.ToString();

        return new DishResponseDto
        {
            Id = dishId,
            Description = dish.Description,
            Name = dish.Name,
            PhotoUrl = dish.PhotoUrl,
        };
    }

    public override async Task<Empty> PutDish(DishPutRequestDto request, ServerCallContext context)
    {
        var ingredientsOfDish = await Task.WhenAll(request.IngredientIds.Select(async x => await _ingredientService.GetByIdAsync(Guid.Parse(x), context.CancellationToken)));

        if (!string.IsNullOrEmpty(request.Id.Id_))
        {
            Guid.TryParse(request.Id.Id_, out var dishId);
            await _dishService.UpdateAsync(dishId, new Domain.Entities.Dish
            {
                Description = request.Description,
                Name = request.Name,
                PhotoUrl = request.PhotoUrl,
                Ingredients = ingredientsOfDish
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