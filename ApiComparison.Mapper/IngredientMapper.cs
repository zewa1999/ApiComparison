using ApiComparison.Contracts.RequestDto;
using ApiComparison.Contracts.ResponseDto;
using ApiComparison.Domain.Entities;

namespace ApiComparison.Mapper;

public class IngredientMapper : IBaseMapper<Ingredient, IngredientRequestDto, IngredientResponseDto>
{
    private readonly IBaseMapper<Dish, DishRequestDto, DishResponseDto> _dishMapper;

    public IngredientMapper(IBaseMapper<Dish, DishRequestDto, DishResponseDto> mapper)
    {
        _dishMapper = mapper;
    }

    public Ingredient RequestToEntity(IngredientRequestDto requestDto)
    {
        var dishIngredientsEntities = new List<Dish>();
        foreach (var dishRequestDto in requestDto.DishIngredientsRequestDto)
        {
            dishIngredientsEntities.Add(_dishMapper.RequestToEntity(dishRequestDto));
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
            dishIngredientsEntities.Add(_dishMapper.EntityToResponse(dish));
        }

        return new IngredientResponseDto
        {
            Name = entity.Name,
            Quantity = entity.Quantity,
            UnitOfMeasure = entity.UnitOfMeasure,
            DishIngredients = dishIngredientsEntities
        };
    }
}
