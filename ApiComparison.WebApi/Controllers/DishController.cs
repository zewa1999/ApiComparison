using ApiComparison.Application.Interfaces.BusinessServices;
using ApiComparison.Contracts.RequestDto;
using ApiComparison.Contracts.ResponseDto;
using ApiComparison.Domain.Entities;
using ApiComparison.Mapping.Base;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ApiComparison.WebApi.Controllers;

[Route("dishes")]
public class DishController : BaseController<IDishService, Dish, DishRequestDto, DishResponseDto>
{
    private readonly IIngredientService _ingredientService;

    public DishController(IDishService service, IMapper<Dish, DishRequestDto, DishResponseDto> mapper, IIngredientService ingredientService) : base(service, mapper)
    {
        _ingredientService = ingredientService;
    }

    public override async Task<IActionResult> Post(DishRequestDto requestDto, CancellationToken cancellationToken)
    {
        var entity = await Service.InsertAsync(await GetMappedEntity(requestDto, cancellationToken), cancellationToken);

        var location = Url.Action(nameof(Get), new { id = entity.Id }) ?? $"/{entity.Id}";
        return Created(location, Mapper.EntityToResponse(entity));
    }

    public override async Task<IActionResult> Put([Required] Guid id, DishRequestDto requestDto, CancellationToken cancellationToken)
    {
        var entity = await GetMappedEntity(requestDto, cancellationToken);

        await Service.UpdateAsync(id, entity, cancellationToken);
        return NoContent();
    }

    private async Task<Dish> GetMappedEntity(DishRequestDto requestDto, CancellationToken cancellationToken)
    {
        var ingredients = new List<Ingredient>();
        foreach (var ingredientId in requestDto.IngredientsIds)
        {
            ingredients.Add(await _ingredientService.GetByIdAsync(ingredientId, cancellationToken));
        }

        var entity = Mapper.RequestToEntity(requestDto);
        entity.Ingredients = ingredients;

        return entity;
    }
}