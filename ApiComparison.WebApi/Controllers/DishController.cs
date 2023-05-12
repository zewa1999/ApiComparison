using ApiComparison.Application.Interfaces.BusinessServices;
using ApiComparison.Contracts.DishDtos;
using ApiComparison.Domain.Entities;
using ApiComparison.Mapping.Base;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ApiComparison.WebApi.Controllers;

[ApiController]
[Route("dishes")]
public class DishController : ControllerBase
{
    private readonly IDishService _service;
    private readonly IDishMapper _mapper;
    private readonly IIngredientMapper _ingredientMapper;
    private readonly IIngredientService _ingredientService;

    public DishController(IDishService service,
                          IDishMapper mapper,
                          IIngredientMapper ingredientMapper,
                          IIngredientService ingredientService)
    {
        _service = service;
        _mapper = mapper;
        _ingredientMapper = ingredientMapper;
        _ingredientService = ingredientService;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] Guid? id, CancellationToken cancellationToken)
    {
        if (id is not null)
        {
            return Ok(_mapper.EntityToResponse(await _service.GetByIdAsync(id, cancellationToken)));
        }

        var entities = await _service.GetAllAsync(cancellationToken);
        var entityDtos = entities.Select(_mapper.EntityToResponse).ToList();

        return Ok(entityDtos);
    }

    [HttpGet]
    [Route("{id:guid}/ingredients")]
    public async Task<IActionResult> GetDishIngredientsAsync(Guid id, CancellationToken cancellationToken)
    {
        var ingredients = await _service.GetIngredientsOfDishes(id, cancellationToken);

        return Ok(ingredients.Select(_ingredientMapper.EntityToResponse));
    }

    [HttpPost]
    public async Task<IActionResult> Post(DishRequestDto requestDto, CancellationToken cancellationToken)
    {
        var entity = await _service.InsertAsync(await GetMappedEntity(requestDto, cancellationToken), cancellationToken);

        var location = Url.Action(nameof(Get), new { id = entity.Id }) ?? $"/{entity.Id}";
        return Created(location, _mapper.EntityToResponse(entity));
    }

    [HttpPut]
    public async Task<IActionResult> Put([Required] Guid id, DishRequestDto requestDto, CancellationToken cancellationToken)
    {
        var entity = await GetMappedEntity(requestDto, cancellationToken);

        await _service.UpdateAsync(id, entity, cancellationToken);
        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([Required][FromQuery] Guid id, CancellationToken cancellationToken)
    {
        await _service.DeleteByIdAsync(id, cancellationToken);
        return NoContent();
    }

    private async Task<Dish> GetMappedEntity(DishRequestDto requestDto, CancellationToken cancellationToken)
    {
        var ingredients = new List<Ingredient>();
        foreach (var ingredientId in requestDto.IngredientsIds)
        {
            ingredients.Add(await _ingredientService.GetByIdAsync(ingredientId, cancellationToken));
        }

        var entity = _mapper.RequestToEntity(requestDto);
        entity.Ingredients = ingredients;

        return entity;
    }
}