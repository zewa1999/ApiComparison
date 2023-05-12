using ApiComparison.Contracts.RequestDto;
using ApiComparison.Contracts.ResponseDto;
using ApiComparison.Domain.Entities;
using ApiComparison.Mapping.Base;

namespace ApiComparison.Mapping.Mappers;

public class DishIngredientMapper : IMapper<Dish, DishRequestDto, DishResponseDto>, IMapper<Ingredient, IngredientRequestDto, IngredientResponseDto>
{
    public Ingredient RequestToEntity(IngredientRequestDto requestDto)
    {
        return new Ingredient
        {
            Name = requestDto.Name,
            Quantity = requestDto.Quantity,
            UnitOfMeasure = requestDto.UnitOfMeasure
        };
    }

    public IngredientResponseDto EntityToResponse(Ingredient entity)
    {
        var dishIngredientsEntities = new List<DishResponseDto>();

        if (entity.Dishes is not null)
        {
            foreach (var dish in entity.Dishes)
            {
                dishIngredientsEntities.Add(EntityToResponse(dish));
            }
        }

        return new IngredientResponseDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Quantity = entity.Quantity,
            UnitOfMeasure = entity.UnitOfMeasure,
            Dishes = dishIngredientsEntities
        };
    }

    public Dish RequestToEntity(DishRequestDto requestDto)
    {
        return new Dish
        {
            Name = requestDto.Name,
            Description = requestDto.Description,
            PhotoUrl = requestDto.PhotoUrl
        };
    }

    public DishResponseDto EntityToResponse(Dish dish)
    {
        var dishIngredientsResponseDtos = new List<IngredientResponseDto>();
        foreach (var entity in dish.Ingredients)
        {
            dishIngredientsResponseDtos.Add(EntityToResponse(entity));
        }

        return new DishResponseDto
        {
            Id = dish.Id,
            Name = dish.Name,
            Description = dish.Description,
            PhotoUrl = dish.PhotoUrl,
            Ingredients = dishIngredientsResponseDtos
        };
    }
}