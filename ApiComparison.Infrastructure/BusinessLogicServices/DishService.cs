using ApiComparison.Application.Interfaces;
using ApiComparison.Contracts.Dto.RequestDto;
using ApiComparison.Contracts.Dto.ResponseDto;
using ApiComparison.Domain.Entities;
using ApiComparison.Domain.Interfaces.Repositories;
using ApiComparison.Mapping.Mappers;
using FluentValidation;

namespace ApiComparison.Infrastructure.BusinessLogicServices;

public class DishService : BaseService<Dish, DishRequestDto, DishResponseDto>, IDishService
{
    public DishService(IDishRepository repository,
                       IValidator<DishRequestDto> validator,
                       IMapper<Dish, DishRequestDto, DishResponseDto> mapper) : base(repository, validator, mapper)
    {
    }
}
