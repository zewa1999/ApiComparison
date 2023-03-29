using ApiComparison.Contracts.RequestDto;
using ApiComparison.Contracts.ResponseDto;

namespace ApiComparison.Application.Interfaces;

public interface IIngredientService : IBaseService<IngredientRequestDto, IngredientResponseDto>
{
}
