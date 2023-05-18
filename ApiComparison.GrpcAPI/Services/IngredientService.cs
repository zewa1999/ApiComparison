using ApiComparison.Application.Interfaces.BusinessServices;
using ApiComparison.Domain.Entities;
using ApiComparison.GrpcAPI;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
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
            ingredient = await _ingredientService.GetByIdAsync(ingredientId, context.CancellationToken);
        }

        var response = new IngredientResponseDto
        {
            Id = request,
            Name = ingredient.Name,
            Quantity = ingredient.Quantity,
            UnitOfMeasure = ingredient.UnitOfMeasure
        };

        return response;
    }

    public override async Task<IngredientListResponseDto> GetIngredients(IngredientRequestDto request, ServerCallContext context)
    {
        var ingredients = await _ingredientService.GetAllAsync(context.CancellationToken);

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
                UnitOfMeasure = ingredient.UnitOfMeasure
            });
        }

        return response;
    }

    public override async Task<DishListResponseDto> GetDishesOfIngredient(Id id, ServerCallContext context)
    {
        Guid.TryParse(id.Id_.ToString(), out var ingredientId);
        var dishes = await _ingredientService.GetDishesOfIngredient(ingredientId, context.CancellationToken);

        var response = new DishListResponseDto();

        foreach (var dish in dishes)
        {
            Id dishId = new Id();
            id.Id_ = dish.Id.ToString();

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

    public override async Task<IngredientResponseDto> PostIngredient(IngredientRequestDto request, ServerCallContext context)
    {
        var ingredient = await _ingredientService.InsertAsync(new Domain.Entities.Ingredient
        {
            Name = request.Name,
            Quantity = request.Quantity,
            UnitOfMeasure = request.UnitOfMeasure
        }, context.CancellationToken);

        Id ingredientId = new Id();
        ingredientId.Id_ = ingredient.Id.ToString();

        return new IngredientResponseDto
        {
            Id = ingredientId,
            Name = ingredient.Name,
            Quantity = ingredient.Quantity,
            UnitOfMeasure = ingredient.UnitOfMeasure
        };
    }

    public override async Task<Empty> PutIngredient(IngredientPutRequestDto request, ServerCallContext context)
    {
        if (!string.IsNullOrEmpty(request.Id.Id_))
        {
            Guid.TryParse(request.Id.Id_, out var ingredientId);
            await _ingredientService.UpdateAsync(ingredientId, new Domain.Entities.Ingredient
            {
                Name = request.Name,
                Quantity = request.Quantity,
                UnitOfMeasure = request.UnitOfMeasure
            }, context.CancellationToken);
        }

        return new Empty();
    }

    public override async Task<Empty> DeleteIngredient(Id request, ServerCallContext context)
    {
        if (!string.IsNullOrEmpty(request.Id_))
        {
            Guid.TryParse(request.Id_, out var ingredientId);
            await _ingredientService.DeleteByIdAsync(ingredientId, context.CancellationToken);
        }

        return new Empty();
    }
}