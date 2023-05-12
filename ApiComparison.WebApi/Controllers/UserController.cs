using ApiComparison.Application.Interfaces.BusinessServices;
using ApiComparison.Contracts.RequestDto;
using ApiComparison.Contracts.ResponseDto;
using ApiComparison.Domain.Entities;
using ApiComparison.Mapping.Base;
using Microsoft.AspNetCore.Mvc;

namespace ApiComparison.WebApi.Controllers;

[Route("users")]
public class UserController : BaseController<IUserService, User, UserRequestDto, UserResponseDto>
{
    public UserController(IUserService service, IMapper<User, UserRequestDto, UserResponseDto> mapper) : base(service, mapper)
    {
    }
}