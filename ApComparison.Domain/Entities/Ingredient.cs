namespace ApiComparison.Domain.Entities;

public record Ingredient : BaseEntity
{
    public Guid IngredientId { get; set; }
    public required string Name { get; set; }
    public required decimal Quantity { get; set; }
    public required string UnitOfMeasure { get; set; }
    public ICollection<Dish> DishIngredients { get; set; } = null!;
}