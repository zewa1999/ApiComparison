﻿using ApiComparison.Application.Interfaces.BusinessServices;
using ApiComparison.Contracts.RequestDto;
using ApiComparison.Contracts.ResponseDto;
using ApiComparison.Domain.Entities;
using ApiComparison.Mapping.Base;
using Microsoft.AspNetCore.Mvc;

namespace ApiComparison.WebApi.Controllers;

[Route("accounts")]
public class AccountController : BaseController<IAccountService, Account, AccountRequestDto, AccountResponseDto>
{
    public AccountController(IAccountService service, IMapper<Account, AccountRequestDto, AccountResponseDto> mapper) : base(service, mapper)
    {
    }
}