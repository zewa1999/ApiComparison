using ApiComparison.Contracts.Dto.RequestDto;
using ApiComparison.Contracts.Dto.ResponseDto;

namespace ApiComparison.Application.Interfaces;

public interface IDishService : IBaseService<DishRequestDto, DishResponseDto>
{
}
