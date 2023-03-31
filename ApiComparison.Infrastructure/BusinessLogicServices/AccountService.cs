using ApiComparison.Application.Interfaces;
using ApiComparison.Contracts.RequestDto;
using ApiComparison.Contracts.ResponseDto;
using ApiComparison.Domain.Entities;
using ApiComparison.Domain.Interfaces.Repositories;
using ApiComparison.Mapper;
using FluentValidation;

namespace ApiComparison.Infrastructure.BusinessLogicServices;

public class AccountService : BaseService<Account, AccountRequestDto, AccountResponseDto>, IAccountService
{
    public AccountService(IBaseRepository<Account> repository,
                          IValidator<AccountRequestDto> validator,
                          IBaseMapper<Account, AccountRequestDto, AccountResponseDto> mapper) : base(repository, validator, mapper)
    {
    }
}
