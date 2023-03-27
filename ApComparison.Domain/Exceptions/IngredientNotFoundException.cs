using ApiComparison.Domain.Entities;

namespace ApiComparison.Domain.Exceptions;

public class IngredientNotFoundException : Exception
{
    public IngredientNotFoundException(BaseEntity baseEntity)
        : base("Ingredient not found!")
    {
    }
}

