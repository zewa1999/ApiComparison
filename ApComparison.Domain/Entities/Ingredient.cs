namespace ApiComparison.Domain.Entities;

public record Ingredient : BaseEntity
{
    public required string Name { get; set; }
    public required double Quantity { get; set; }
    public required string UnitOfMeasure { get; set; }
    public ICollection<Dish> DishIngredients { get; set; } = null!;
}