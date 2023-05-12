using ApiComparison.Application.Interfaces.BusinessServices;
using ApiComparison.Contracts.RequestDto;
using ApiComparison.Contracts.ResponseDto;
using ApiComparison.Domain.Entities;
using ApiComparison.Domain.Interfaces.Repositories;
using FluentValidation;

namespace ApiComparison.Infrastructure.BusinessLogicServices;

public class IngredientService : BaseService<Ingredient>, IIngredientService
{
    public IngredientService(IIngredientRepository repository, IValidator<Ingredient> validator) : base(repository, validator)
    {
    }
}