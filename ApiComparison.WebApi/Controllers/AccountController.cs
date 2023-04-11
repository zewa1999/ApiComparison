using ApiComparison.Application.Interfaces;
using ApiComparison.Contracts.RequestDto;
using ApiComparison.Contracts.ResponseDto;
using ApiComparison.Domain.Entities;
using ApiComparison.Mapping.Base;

namespace ApiComparison.WebApi.Controllers;

public class AccountController : BaseController<IAccountService, Account, AccountRequestDto, AccountResponseDto>
{
    public AccountController(IAccountService service, IMapper<Account, AccountRequestDto, AccountResponseDto> mapper) : base(service, mapper)
    {
    }
}