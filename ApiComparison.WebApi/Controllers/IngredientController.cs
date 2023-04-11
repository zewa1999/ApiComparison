using ApiComparison.Application.Interfaces;
using ApiComparison.Contracts.RequestDto;
using ApiComparison.Contracts.ResponseDto;
using ApiComparison.Domain.Entities;
using ApiComparison.Mapping.Base;

namespace ApiComparison.WebApi.Controllers;

public class IngredientController : BaseController<IIngredientService, Ingredient, IngredientRequestDto, IngredientResponseDto>
{
    public IngredientController(IIngredientService service, IMapper<Ingredient, IngredientRequestDto, IngredientResponseDto> mapper) : base(service, mapper)
    {
    }
}