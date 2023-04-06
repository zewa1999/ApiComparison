namespace ApiComparison.Contracts.ResponseDto;

public record DishResponseDto : BaseResponseDto
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string PhotoUrl { get; set; }
    public ICollection<IngredientResponseDto> DishIngredientsResponseDto { get; set; } = null!;
}