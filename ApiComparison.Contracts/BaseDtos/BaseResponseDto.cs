namespace ApiComparison.Contracts.BaseDtos;

public record BaseResponseDto
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastUpdatedAt { get; set; }
}