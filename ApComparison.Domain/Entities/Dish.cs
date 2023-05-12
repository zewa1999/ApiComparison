namespace ApiComparison.Domain.Entities;

public record Dish : BaseEntity
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string PhotoUrl { get; set; }
    public ICollection<Ingredient> Ingredients { get; set; } = null!;
}