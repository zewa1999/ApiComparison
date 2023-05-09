using ApiComparison.Application.Interfaces;
using ApiComparison.Domain.Entities;
using ApiComparison.Domain.Interfaces.Repositories;
using FluentValidation;

namespace ApiComparison.Infrastructure.BusinessLogicServices;

public class UserService : BaseService<User>, IUserService
{
    public UserService(IUserRepository repository,
                       IValidator<User> validator) : base(repository, validator)
    {
    }
}