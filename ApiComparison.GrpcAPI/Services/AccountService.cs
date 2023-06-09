﻿using ApiComparison.Application.Interfaces.BusinessServices;
using ApiComparison.GrpcAPI;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace ApiComparison.GrpcApi.Services;

public class AccountService : Account.AccountBase
{
    private readonly IAccountService _accountService;

    public AccountService(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public override async Task<AccountResponseDto> GetAccount(Id request, ServerCallContext context)
    {
        Domain.Entities.Account account = null!;
        Id id = new Id();
        if (!string.IsNullOrEmpty(request.Id_))
        {
            Guid.TryParse(request.Id_, out var accountId);
            account = await _accountService.GetByIdAsync(accountId, context.CancellationToken);
            id.Id_ = account.Id.ToString();
        }


        return new AccountResponseDto()
        {
            Id = id,
            Email = account.Email,
            Password = account.Password,
            Username = account.Username,
        };
    }

    public override async Task<AccountListResponseDto> GetAccounts(AccountRequestDto request, ServerCallContext context)
    {
        var accounts = await _accountService.GetAllAsync(context.CancellationToken);

        var accountListResponseDto = new AccountListResponseDto();

        foreach (var account in accounts)
        {
            Id id = new Id();
            id.Id_ = account.Id.ToString();

            accountListResponseDto.Items.Add(new AccountResponseDto
            {
                Id = id,
                Email = account.Email,
                Password = account.Password,
                Username = account.Username,
            });
        }

        return accountListResponseDto;
    }

    public override async Task<AccountResponseDto> PostAccount(AccountRequestDto request, ServerCallContext context)
    {
        var account = await _accountService.InsertAsync(new Domain.Entities.Account
        {
            Email = request.Email,
            Password = request.Password,
            Username = request.Username
        }, context.CancellationToken);

        Id id = new Id();
        id.Id_ = account.Id.ToString();

        return new AccountResponseDto()
        {
            Id = id,
            Email = account.Email,
            Password = account.Password,
            Username = account.Username,
        };
    }

    public override async Task<Empty> PutAccount(AccountPutRequestDto request, ServerCallContext context)
    {
        if (!string.IsNullOrEmpty(request.Id.Id_))
        {
            Guid.TryParse(request.Id.Id_, out var accountId);
            await _accountService.UpdateAsync(accountId, new Domain.Entities.Account
            {
                Email = request.Email,
                Password = request.Password,
                Username = request.Username
            }, context.CancellationToken);
        }

        return new Empty();
    }

    public override async Task<Empty> DeleteAccount(Id request, ServerCallContext context)
    {
        if (!string.IsNullOrEmpty(request.Id_))
        {
            Guid.TryParse(request.Id_, out var accountId);
            await _accountService.DeleteByIdAsync(accountId, context.CancellationToken);
        }

        return new Empty();
    }
}