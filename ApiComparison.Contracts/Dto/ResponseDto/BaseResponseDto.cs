using ApiComparison.Contracts.Dto;

namespace ApiComparison.Contracts.Dto.ResponseDto;

public record BaseResponseDto : BaseDto
{
    public DateTime CreatedAt { get; set; }
    public DateTime LastUpdatedAt { get; set; }
    public DateTime DeletedAt { get; set; }
}
