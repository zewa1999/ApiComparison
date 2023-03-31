namespace ApiComparison.Contracts.Dto.ResponseDto;

public record UserResponseDto : BaseResponseDto
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Bio { get; set; }
    public required AccountResponseDto AccountResponseDto { get; set; }
    public required AddressResponseDto AddressResponseDto { get; set; }
}
