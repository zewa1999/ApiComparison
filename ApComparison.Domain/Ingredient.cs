namespace ApiComparison.Domain;

public class Ingredient : BaseEntity
{
    public required string Name { get; set; }
    public required decimal Quantity { get; set; }
    public required string UnitOfMeasure { get; set; }
    public ICollection<Dish> DishIngredients { get; set; } = null!;
}
