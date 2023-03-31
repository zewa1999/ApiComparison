using ApiComparison.Contracts.Dto.RequestDto;
using ApiComparison.Contracts.Dto.ResponseDto;

namespace ApiComparison.Application.Interfaces;

public interface IIngredientService : IBaseService<IngredientRequestDto, IngredientResponseDto>
{
}
