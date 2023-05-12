using ApiComparison.Application.Interfaces.BusinessServices;
using ApiComparison.Contracts.RequestDto;
using ApiComparison.Contracts.ResponseDto;
using ApiComparison.Domain.Entities;
using ApiComparison.Mapping.Base;
using Microsoft.AspNetCore.Mvc;

namespace ApiComparison.WebApi.Controllers;

[Route("ingredients")]
public class IngredientController : BaseController<IIngredientService, Ingredient, IngredientRequestDto, IngredientResponseDto>
{
    public IngredientController(IIngredientService service, IMapper<Ingredient, IngredientRequestDto, IngredientResponseDto> mapper) : base(service, mapper)
    {
    }
}