using ApiComparison.Application.Interfaces.BusinessServices;
using ApiComparison.Contracts.AddressDtos;
using ApiComparison.Mapping.Base;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ApiComparison.WebApi.Controllers;

[ApiController]
[Route("addresses")]
public class AddressController : ControllerBase
{
    private readonly IAddressService _service;
    private readonly IAddressMapper _mapper;

    public AddressController(IAddressService service, IAddressMapper mapper)
    {
        _service = service;
        _mapper = mapper;
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

    [HttpPost]
    public async Task<IActionResult> Post(AddressRequestDto requestDto, CancellationToken cancellationToken)
    {
        var entity = await _service.InsertAsync(_mapper.RequestToEntity(requestDto), cancellationToken);

        var location = Url.Action(nameof(Get), new { id = entity.Id }) ?? $"/{entity.Id}";
        return Created(location, _mapper.EntityToResponse(entity));
    }

    [HttpPut("{id}")]
    public virtual async Task<IActionResult> Put([Required] Guid id, AddressRequestDto requestDto, CancellationToken cancellationToken)
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