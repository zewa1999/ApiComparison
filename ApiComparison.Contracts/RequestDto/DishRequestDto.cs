namespace ApiComparison.Contracts.RequestDto;

public record DishRequestDto : BaseRequestDto
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string PhotoUrl { get; set; }
    public ICollection<Guid> IngredientsIds { get; set; } = null!;
}