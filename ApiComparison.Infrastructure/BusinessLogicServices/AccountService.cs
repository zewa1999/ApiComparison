using ApiComparison.Application.Interfaces.BusinessServices;
using ApiComparison.Contracts.RequestDto;
using ApiComparison.Contracts.ResponseDto;
using ApiComparison.Domain.Entities;
using ApiComparison.Domain.Interfaces.Repositories;
using FluentValidation;

namespace ApiComparison.Infrastructure.BusinessLogicServices;

public class AccountService : BaseService<Account>, IAccountService
{
    public AccountService(IAccountRepository repository,
                          IValidator<Account> validator) : base(repository, validator)
    {
    }
}