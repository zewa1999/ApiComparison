namespace ApiComparison.Contracts.RequestDto;

public record DishRequestDto : BaseRequestDto
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string PhotoUrl { get; set; }
    public ICollection<IngredientRequestDto> DishIngredientsRequestDto { get; set; } = null!;
}
