using ApiComparison.Application.Interfaces;
using ApiComparison.Contracts.RequestDto;
using ApiComparison.Contracts.ResponseDto;
using ApiComparison.Domain.Entities;
using ApiComparison.Mapping.Base;

namespace ApiComparison.WebApi.Controllers;

public class DishController : BaseController<IDishService, Dish, DishRequestDto, DishResponseDto>
{
    public DishController(IDishService service, IMapper<Dish, DishRequestDto, DishResponseDto> mapper) : base(service, mapper)
    {
    }
}