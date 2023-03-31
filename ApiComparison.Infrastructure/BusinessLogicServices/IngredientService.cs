using ApiComparison.Application.Interfaces;
using ApiComparison.Contracts.RequestDto;
using ApiComparison.Contracts.ResponseDto;
using ApiComparison.Domain.Entities;
using ApiComparison.Domain.Interfaces.Repositories;
using ApiComparison.Mapper;
using FluentValidation;

namespace ApiComparison.Infrastructure.BusinessLogicServices;

public class IngredientService : BaseService<Ingredient, IngredientRequestDto, IngredientResponseDto>, IIngredientService
{
    public IngredientService(IBaseRepository<Ingredient> repository,
                             IValidator<IngredientRequestDto> validator,
                             IBaseMapper<Ingredient, IngredientRequestDto, IngredientResponseDto> mapper) : base(repository, validator, mapper)
    {
    }
}
