﻿using ApiComparison.Domain.Entities;

namespace ApiComparison.Application.Interfaces.BusinessServices;

public interface IDishService : IBaseService<Dish>
{
    public Task<IEnumerable<Ingredient>> GetIngredientsOfDishes(Guid? entityId, CancellationToken cancellationToken);
}