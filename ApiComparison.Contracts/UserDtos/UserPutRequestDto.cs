using ApiComparison.Contracts.BaseDtos;

namespace ApiComparison.Contracts.UserDtos;

public record UserPutRequestDto : BaseRequestDto
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Bio { get; set; }
}
