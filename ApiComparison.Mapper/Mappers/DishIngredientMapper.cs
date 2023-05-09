using ApiComparison.Contracts.RequestDto;
using ApiComparison.Contracts.ResponseDto;
using ApiComparison.Domain.Entities;
using ApiComparison.Mapping.Base;

namespace ApiComparison.Mapping.Mappers;

public class DishIngredientMapper : IMapper<Dish, DishRequestDto, DishResponseDto>, IMapper<Ingredient, IngredientRequestDto, IngredientResponseDto>
{
    public Ingredient RequestToEntity(IngredientRequestDto requestDto)
    {
        var dishIngredientsEntities = new List<Dish>();
        //foreach (var dishRequestDto in dishRequestDtos)
        //{
        //    dishIngredientsEntities.Add(RequestToEntity(dishRequestDto, null));
        //}

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
            Id = entity.Id,
            Name = entity.Name,
            Quantity = entity.Quantity,
            UnitOfMeasure = entity.UnitOfMeasure,
            DishIngredients = dishIngredientsEntities
        };
    }

    public Dish RequestToEntity(DishRequestDto requestDto)
    {
        var dishIngredientsEntities = new List<Ingredient>();
        // asta trebuie refacut
        //foreach (var ingredientRequestDto in requestDtos)
        //{
        //    dishIngredientsEntities.Add(RequestToEntity(ingredientRequestDto, null));
        //}

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
            Id = dish.Id,
            Name = dish.Name,
            Description = dish.Description,
            PhotoUrl = dish.PhotoUrl,
            DishIngredientsResponseDto = dishIngredientsResponseDtos
        };
    }
}