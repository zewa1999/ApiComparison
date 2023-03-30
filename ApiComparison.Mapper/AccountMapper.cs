﻿using ApiComparison.Contracts.RequestDto;
using ApiComparison.Contracts.ResponseDto;
using ApiComparison.Domain.Entities;

namespace ApiComparison.Mapper;

public class AccountMapper : IBaseMapper<Account, AccountRequestDto, AccountResponseDto>
{
    public Account RequestToEntity(AccountRequestDto requestDto)
    {
        return new Account
        {
            Username = requestDto.Username,
            Password = requestDto.Password,
            Email = requestDto.Email
        };
    }

    public AccountResponseDto EntityToResponse(Account account)
    {
        return new AccountResponseDto
        {
            Username = account.Username,
            Password = account.Password,
            Email = account.Email
        };
    }
}