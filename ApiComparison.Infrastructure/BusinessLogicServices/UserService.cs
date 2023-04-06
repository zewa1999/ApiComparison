using ApiComparison.Application.Interfaces;
using ApiComparison.Contracts.RequestDto;
using ApiComparison.Contracts.ResponseDto;
using ApiComparison.Domain.Entities;
using ApiComparison.Domain.Interfaces.Repositories;
using FluentValidation;

namespace ApiComparison.Infrastructure.BusinessLogicServices;

public class UserService : BaseService<User, UserRequestDto, UserResponseDto>, IUserService
{
    public UserService(IUserRepository repository,
                       IValidator<User> validator) : base(repository, validator)
    {
    }
}