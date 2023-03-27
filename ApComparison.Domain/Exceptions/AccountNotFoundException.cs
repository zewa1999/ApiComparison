using ApiComparison.Domain.Entities;

namespace ApiComparison.Domain.Exceptions;

public class AccountNotFoundException : Exception
{
    public AccountNotFoundException(BaseEntity baseEntity)
        : base("Account not found!")
    {
    }
}

