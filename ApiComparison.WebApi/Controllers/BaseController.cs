using ApiComparison.Application.Interfaces;
using ApiComparison.Contracts.RequestDto;
using ApiComparison.Contracts.ResponseDto;
using ApiComparison.Domain.Entities;
using ApiComparison.Mapping.Base;
using ApiComparison.WebApi.Filters;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ApiComparison.WebApi.Controllers;

// Return Problem
// Testat controllere
// Grpc Services
// GraphQl Services
// Pagination
// Authentication and Authorizationr + API Key authentication.;
// Caching
// Docker
// Replica Set for DBs(Idk if caching also, but we'll see
// Kubernetes
// Frontend
[ApiController]
[Route("[controller]")]
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
    public async Task<IActionResult> GetById([FromQuery] Guid? id, CancellationToken cancellationToken)
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
    public async Task<IActionResult> Post(TRequestDto requestDto, CancellationToken cancellationToken)
    {
        var entity = await Service.InsertAsync(Mapper.RequestToEntity(requestDto), cancellationToken);

        var location = Url.Action(nameof(GetById), new { id = entity.Id }) ?? $"/{entity.Id}";
        return Created(location, Mapper.EntityToResponse(entity));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put([Required] Guid id, TRequestDto requestDto, CancellationToken cancellationToken)
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