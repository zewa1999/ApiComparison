namespace ApiComparison.Contracts.ResponseDto;

public record UserResponseDto : BaseResponseDto
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Bio { get; set; }
    public required AccountResponseDto Account { get; set; }
    public required AddressResponseDto Address { get; set; }
    public required IEnumerable<DishResponseDto> Dishes { get; set; }
}