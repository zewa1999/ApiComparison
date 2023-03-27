using ApiComparison.Domain.Entities;

namespace ApiComparison.Domain.Exceptions;

public class UserNotFoundException : Exception
{
    public UserNotFoundException(BaseEntity baseEntity)
        : base("User not found!")
    {
    }
}

