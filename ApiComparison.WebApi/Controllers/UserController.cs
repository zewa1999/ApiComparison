using ApiComparison.Application.Interfaces;
using ApiComparison.Contracts.RequestDto;
using ApiComparison.Contracts.ResponseDto;
using ApiComparison.Domain.Entities;
using ApiComparison.Mapping.Mappers;

namespace ApiComparison.WebApi.Controllers;

public class UserController : BaseController<IUserService, User, UserRequestDto, UserResponseDto>
{
    public UserController(IUserService service, IMapper<User, UserRequestDto, UserResponseDto> mapper) : base(service, mapper)
    {
    }
}