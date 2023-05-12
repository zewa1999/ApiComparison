using ApiComparison.Application.Interfaces.BusinessServices;
using ApiComparison.Contracts.UserDtos;
using ApiComparison.Mapping.Base;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ApiComparison.WebApi.Controllers;

[ApiController]
[Route("users")]
public class UserController : ControllerBase
{
    private readonly IUserService _service;
    private readonly IAccountService _accountService;
    private readonly IAddressService _addressService;
    private readonly IUserMapper _mapper;
    private readonly IDishMapper _dishMapper;
    private readonly IAddressMapper _addressMapper;
    private readonly IAccountMapper _accountMapper;

    public UserController(IUserService service,
                          IAccountService accountService,
                          IAddressService addressService,
                          IUserMapper mapper,
                          IDishMapper dishMapper,
                          IAddressMapper addressMapper,
                          IAccountMapper accountMapper)
    {
        _dishMapper = dishMapper;
        _addressMapper = addressMapper;
        _accountMapper = accountMapper;
        _service = service;
        _accountService = accountService;
        _addressService = addressService;
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

    [HttpGet]
    [Route("{id:guid}/address")]
    public async Task<IActionResult> GetUserAddress(Guid id, CancellationToken cancellationToken)
    {
        var address = await _service.GetUserAddress(id, cancellationToken);

        return Ok(_addressMapper.EntityToResponse(address));
    }

    [HttpGet]
    [Route("{id:guid}/account")]
    public async Task<IActionResult> GetUserAccount(Guid id, CancellationToken cancellationToken)
    {
        var account = await _service.GetUserAccount(id, cancellationToken);

        return Ok(_accountMapper.EntityToResponse(account));
    }

    [HttpGet]
    [Route("{id:guid}/dishes")]
    public async Task<IActionResult> GetUserDishes(Guid id, CancellationToken cancellationToken)
    {
        var dishes = await _service.GetUserDishes(id, cancellationToken);

        return Ok(dishes.Select(_dishMapper.EntityToResponse));
    }

    [HttpPost]
    public async Task<IActionResult> Post(UserCreateRequestDto requestDto, CancellationToken cancellationToken)
    {
        var entity = await _service.InsertAsync(_mapper.RequestToEntity(requestDto), cancellationToken);

        var location = Url.Action(nameof(Get), new { id = entity.Id }) ?? $"/{entity.Id}";
        return Created(location, _mapper.EntityToResponse(entity));
    }

    [HttpPut("{id}")]
    public virtual async Task<IActionResult> Put([Required] Guid id, UserRequestDto requestDto, CancellationToken cancellationToken)
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