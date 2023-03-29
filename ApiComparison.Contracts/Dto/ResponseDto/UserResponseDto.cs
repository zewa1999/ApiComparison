namespace ApiComparison.Contracts.ResponseDto;

public record UserResponseDto : BaseResponseDto
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Bio { get; set; }
    public Guid AccountId { get; set; }
    public Guid AddressId { get; set; }
}
