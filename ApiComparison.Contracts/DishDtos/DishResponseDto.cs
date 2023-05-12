using ApiComparison.Contracts.BaseDtos;

namespace ApiComparison.Contracts.DishDtos;

public record DishResponseDto : BaseResponseDto
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string PhotoUrl { get; set; }
}