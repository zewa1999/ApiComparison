using ApiComparison.Domain.Entities;

namespace ApiComparison.Domain.Exceptions;

public class DishNotFoundException : Exception
{
    public DishNotFoundException(BaseEntity baseEntity)
        : base("Dish not found!")
    {
    }
}

