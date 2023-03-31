namespace ApiComparison.Contracts.Dto.RequestDto;

public record IngredientRequestDto : BaseRequestDto
{
    public required string Name { get; set; }
    public required decimal Quantity { get; set; }
    public required string UnitOfMeasure { get; set; }
    public ICollection<DishRequestDto> DishIngredientsRequestDto { get; set; } = null!;
}
