namespace ApiComparison.Contracts.ResponseDto;

public abstract record BaseResponseDto
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastUpdatedAt { get; set; }
    public DateTime DeletedAt { get; set; }
}