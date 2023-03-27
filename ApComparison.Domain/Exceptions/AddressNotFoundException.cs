using ApiComparison.Domain.Entities;

namespace ApiComparison.Domain.Exceptions;

public class AddressNotFoundException: Exception
{
    public AddressNotFoundException(BaseEntity baseEntity)
        : base("Address not found!")
    {
    }
}

