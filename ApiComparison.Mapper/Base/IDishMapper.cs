using ApiComparison.Contracts.DishDtos;
using ApiComparison.Domain.Entities;

namespace ApiComparison.Mapping.Base;

public interface IDishMapper : IMapper<Dish, DishRequestDto, DishResponseDto>
{
}
