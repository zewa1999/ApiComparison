﻿namespace ApiComparison.Contracts.ResponseDto;

public record IngredientResponseDto : BaseResponseDto
{
    public required string Name { get; set; }
    public required double Quantity { get; set; }
    public required string UnitOfMeasure { get; set; }
    public ICollection<DishResponseDto> DishIngredients { get; set; } = null!;
}