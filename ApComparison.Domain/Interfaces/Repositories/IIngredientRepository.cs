﻿using ApiComparison.Domain.Entities;

namespace ApiComparison.Domain.Interfaces.Repositories;

public interface IIngredientRepository : IBaseRepository<Ingredient>
{
    public Task<IEnumerable<Dish>> GetIngredientDishes(Guid? entityId, CancellationToken cancellationToken)
}