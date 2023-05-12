using ApiComparison.Application.Interfaces.BusinessServices;
using ApiComparison.Contracts.IngredientDtos;
using ApiComparison.Mapping.Base;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ApiComparison.WebApi.Controllers;

[ApiController]
[Route("ingredients")]
public class IngredientController : ControllerBase
{
    private readonly IIngredientService _service;
    private readonly IIngredientMapper _mapper;
    private readonly IDishMapper _dishMapper;

    public IngredientController(IIngredientService service,
                                IIngredientMapper mapper,
                                IDishMapper dishMapper)
    { 
        _service = service;
        _mapper = mapper;
        _dishMapper = dishMapper;
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
    [Route("{id:guid}/dishes")]
    public async Task<IActionResult> GetDishesOfIngredient(Guid id, CancellationToken cancellationToken)
    {
        var ingredients = await _service.GetDishesOfIngredient(id, cancellationToken);

        return Ok(ingredients.Select(_dishMapper.EntityToResponse));
    }


    [HttpPost]
    public async Task<IActionResult> Post(IngredientRequestDto requestDto, CancellationToken cancellationToken)
    {
        var entity = await _service.InsertAsync(_mapper.RequestToEntity(requestDto), cancellationToken);

        var location = Url.Action(nameof(Get), new { id = entity.Id }) ?? $"/{entity.Id}";
        return Created(location, _mapper.EntityToResponse(entity));
    }

    [HttpPut("{id}")]
    public virtual async Task<IActionResult> Put([Required] Guid id, IngredientRequestDto requestDto, CancellationToken cancellationToken)
    {
        await _service.UpdateAsync(id, _mapper.RequestToEntity(requestDto), cancellationToken);
        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([Required][FromQuery] Guid id, CancellationToken cancellationToken)
    {
        await _service.DeleteByIdAsync(id, cancellationToken);
        return NoContent();
    }

}