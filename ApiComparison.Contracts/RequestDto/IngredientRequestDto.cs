namespace ApiComparison.Contracts.RequestDto;

public record IngredientRequestDto : BaseRequestDto
{
    public required string Name { get; set; }
    public required double Quantity { get; set; }
    public required string UnitOfMeasure { get; set; }
    public ICollection<Guid> DishIngredientsIds { get; set; } = null!;
}