using ApiComparison.Contracts.BaseDtos;

namespace ApiComparison.Contracts.DishDtos;

public record DishRequestDto : BaseRequestDto
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string PhotoUrl { get; set; }
    public HashSet<Guid> IngredientsIds { get; set; } = null!;
}