using ApiComparison.Domain.Entities;

namespace ApiComparison.Domain.Exceptions;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(Type entityType)
        : base($"{GetEntityType(entityType)} not found!")
    {
    }

    public static string GetEntityType(Type entityType)
    {
        return entityType.Name switch
        {
            nameof(Account) => "Account",
            nameof(Address) => "Address",
            nameof(Ingredient) => "Ingredient",
            nameof(User) => "User",
            _ => "Entity"
        };
    }
}