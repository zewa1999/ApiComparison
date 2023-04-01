namespace ApiComparison.Domain.Entities;

public record Dish : BaseEntity
{
    public Guid DishId { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string PhotoUrl { get; set; }
    public ICollection<Ingredient> DishIngredients { get; set; } = null!;
}
