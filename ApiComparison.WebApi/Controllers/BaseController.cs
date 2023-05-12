using ApiComparison.Application.Interfaces.BusinessServices;
using ApiComparison.Contracts.RequestDto;
using ApiComparison.Contracts.ResponseDto;
using ApiComparison.Domain.Entities;
using ApiComparison.Mapping.Base;
using ApiComparison.WebApi.Filters;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ApiComparison.WebApi.Controllers;

// tranzactii pentru adaugarea in db la user(adica si account, daca crapa una, e revert)
// Return Problem
// Testat controllere
// Pagination
// Authentication and Authorizationr + API Key authentication.;
// Caching
// Docker
// Replica Set for DBs(Idk if caching also, but we'll see
// Kubernetes
// Frontend

// refactorizare, daca o proprietate e nula sau empty nu o adaug si de facut endpointi gen: /dish/{id}/ingredients si unde mai e nevoie
[ApiController]
[TypeFilter(typeof(AggregateExceptionFilterAttribute))]
[TypeFilter(typeof(EntityNotFoundExceptionFilterAttribute))]
public abstract class BaseController<TService, TEntity, TRequestDto, TResponseDto> : ControllerBase
    where TService : IBaseService<TEntity>
    where TEntity : BaseEntity
    where TRequestDto : BaseRequestDto
    where TResponseDto : BaseResponseDto
{
    protected TService Service { get; private set; } = default!;
    protected IMapper<TEntity, TRequestDto, TResponseDto> Mapper { get; private set; } = default!;

    protected BaseController(TService service, IMapper<TEntity, TRequestDto, TResponseDto> mapper)
    {
        Service = service;
        Mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] Guid? id, CancellationToken cancellationToken)
    {
        if (id is not null)
        {
            return Ok(Mapper.EntityToResponse(await Service.GetByIdAsync(id, cancellationToken)));
        }

        var entities = await Service.GetAllAsync(cancellationToken);
        var entityDtos = entities.Select(Mapper.EntityToResponse).ToList();

        return Ok(entityDtos);
    }

    [HttpPost]
    public virtual async Task<IActionResult> Post(TRequestDto requestDto, CancellationToken cancellationToken)
    {
        var entity = await Service.InsertAsync(Mapper.RequestToEntity(requestDto), cancellationToken);

        var location = Url.Action(nameof(Get), new { id = entity.Id }) ?? $"/{entity.Id}";
        return Created(location, Mapper.EntityToResponse(entity));
    }

    [HttpPut("{id}")]
    public virtual async Task<IActionResult> Put([Required] Guid id, TRequestDto requestDto, CancellationToken cancellationToken)
    {
        await Service.UpdateAsync(id, Mapper.RequestToEntity(requestDto), cancellationToken);
        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> Put([Required][FromQuery] Guid id, CancellationToken cancellationToken)
    {
        await Service.DeleteByIdAsync(id, cancellationToken);
        return NoContent();
    }
}