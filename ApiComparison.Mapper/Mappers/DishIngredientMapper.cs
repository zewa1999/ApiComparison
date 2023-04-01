using ApiComparison.Contracts.Dto.RequestDto;
using ApiComparison.Contracts.Dto.ResponseDto;
using ApiComparison.Domain.Entities;

namespace ApiComparison.Mapping.Mappers;

public class DishIngredientMapper : IBaseMapper<Dish, DishRequestDto, DishResponseDto>, IBaseMapper<Ingredient, IngredientRequestDto, IngredientResponseDto>
{
    public Ingredient RequestToEntity(IngredientRequestDto requestDto)
    {
        var dishIngredientsEntities = new List<Dish>();
        foreach (var dishRequestDto in requestDto.DishIngredientsRequestDto)
        {
            dishIngredientsEntities.Add(RequestToEntity(dishRequestDto));
        }

        return new Ingredient
        {
            Name = requestDto.Name,
            Quantity = requestDto.Quantity,
            UnitOfMeasure = requestDto.UnitOfMeasure,
            DishIngredients = dishIngredientsEntities
        };
    }

    public IngredientResponseDto EntityToResponse(Ingredient entity)
    {
        var dishIngredientsEntities = new List<DishResponseDto>();
        foreach (var dish in entity.DishIngredients)
        {
            dishIngredientsEntities.Add(EntityToResponse(dish));
        }

        return new IngredientResponseDto
        {
            Name = entity.Name,
            Quantity = entity.Quantity,
            UnitOfMeasure = entity.UnitOfMeasure,
            DishIngredients = dishIngredientsEntities
        };
    }

    public Dish RequestToEntity(DishRequestDto requestDto)
    {
        var dishIngredientsEntities = new List<Ingredient>();
        foreach (var ingredientRequestDto in requestDto.DishIngredientsRequestDto)
        {
            dishIngredientsEntities.Add(RequestToEntity(ingredientRequestDto));
        }

        return new Dish
        {
            Name = requestDto.Name,
            Description = requestDto.Description,
            PhotoUrl = requestDto.PhotoUrl,
            DishIngredients = dishIngredientsEntities
        };
    }

    public DishResponseDto EntityToResponse(Dish dish)
    {
        var dishIngredientsResponseDtos = new List<IngredientResponseDto>();
        foreach (var entity in dish.DishIngredients)
        {
            dishIngredientsResponseDtos.Add(EntityToResponse(entity));
        }

        return new DishResponseDto
        {
            Name = dish.Name,
            Description = dish.Description,
            PhotoUrl = dish.PhotoUrl,
            DishIngredientsResponseDto = dishIngredientsResponseDtos
        };
    }
}
