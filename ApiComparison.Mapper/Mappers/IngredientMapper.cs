using ApiComparison.Contracts.IngredientDtos;
using ApiComparison.Domain.Entities;
using ApiComparison.Mapping.Base;

namespace ApiComparison.Mapping.Mappers;

internal class IngredientMapper : IIngredientMapper
{
    public Ingredient RequestToEntity(IngredientRequestDto requestDto)
    {
        return new Ingredient
        {
            Name = requestDto.Name,
            Quantity = requestDto.Quantity,
            UnitOfMeasure = requestDto.UnitOfMeasure
        };
    }

    public IngredientResponseDto EntityToResponse(Ingredient entity)
    {
        return new IngredientResponseDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Quantity = entity.Quantity,
            UnitOfMeasure = entity.UnitOfMeasure
        };
    }
}