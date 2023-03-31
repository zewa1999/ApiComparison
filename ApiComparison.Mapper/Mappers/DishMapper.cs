using ApiComparison.Contracts.Dto.RequestDto;
using ApiComparison.Contracts.Dto.ResponseDto;
using ApiComparison.Domain.Entities;
using ApiComparison.Mapping.Mappers;

public class DishMapper : IBaseMapper<Dish, DishRequestDto, DishResponseDto>
{
    private readonly IBaseMapper<Ingredient, IngredientRequestDto, IngredientResponseDto> _ingredientMapper;


    public DishMapper(IBaseMapper<Ingredient, IngredientRequestDto, IngredientResponseDto> mapper)
    {
        _ingredientMapper = mapper;
    }

    public Dish RequestToEntity(DishRequestDto requestDto)
    {
        //var dishIngredientsEntities = requestDto.DishIngredientsRequestDto
        //     .Select(ingredientRequestDto => _ingredientMapper.RequestToEntity(ingredientRequestDto))
        //     .ToList();

        var dishIngredientsEntities = new List<Ingredient>();
        foreach (var ingredientRequestDto in requestDto.DishIngredientsRequestDto)
        {
            dishIngredientsEntities.Add(_ingredientMapper.RequestToEntity(ingredientRequestDto));
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
            dishIngredientsResponseDtos.Add(_ingredientMapper.EntityToResponse(entity));
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