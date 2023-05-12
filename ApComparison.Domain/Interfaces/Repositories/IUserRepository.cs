using ApiComparison.Domain.Entities;

namespace ApiComparison.Domain.Interfaces.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    public Task<Address?> GetUserAddress(Guid? entityId, CancellationToken cancellationToken);

    public Task<IEnumerable<Dish>> GetUserDishes(Guid? entityId, CancellationToken cancellationToken);
}