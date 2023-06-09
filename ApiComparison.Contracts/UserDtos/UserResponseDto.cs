﻿using ApiComparison.Contracts.BaseDtos;

namespace ApiComparison.Contracts.UserDtos;

public record UserResponseDto : BaseResponseDto
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Bio { get; set; }
}
