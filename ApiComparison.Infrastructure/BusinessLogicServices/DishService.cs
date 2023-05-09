using ApiComparison.Application.Interfaces;
using ApiComparison.Domain.Entities;
using ApiComparison.Domain.Interfaces.Repositories;
using FluentValidation;

namespace ApiComparison.Infrastructure.BusinessLogicServices;

public class DishService : BaseService<Dish>, IDishService
{
    public DishService(IDishRepository repository,
                       IValidator<Dish> validator) : base(repository, validator)
    {
    }
}