using ApiComparison.Application.Interfaces;
using ApiComparison.Contracts.Dto.RequestDto;
using ApiComparison.Contracts.Dto.ResponseDto;
using ApiComparison.Domain.Entities;
using ApiComparison.Domain.Interfaces.Repositories;
using ApiComparison.Mapping.Mappers;
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
