using ApiComparison.Contracts.IngredientDtos;
using ApiComparison.Domain.Entities;

namespace ApiComparison.Mapping.Base;

public interface IIngredientMapper : IMapper<Ingredient, IngredientRequestDto, IngredientResponseDto>
{
}
