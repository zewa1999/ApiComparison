using ApiComparison.Contracts.BaseDtos;

namespace ApiComparison.Contracts.IngredientDtos;

public record IngredientResponseDto : BaseResponseDto
{
    public required string Name { get; set; }
    public required double Quantity { get; set; }
    public required string UnitOfMeasure { get; set; }
}