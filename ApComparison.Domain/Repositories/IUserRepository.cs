using ApiComparison.Domain.Entities;

namespace ApiComparison.Domain.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    public Task<Address> GetUserAddressAsync(Guid? entityId, CancellationToken cancellationToken);

    public Task<Account> GetUserAccountAsync(Guid? entityId, CancellationToken cancellationToken);

    public Task<IEnumerable<Dish>> GetUserDishesAsync(Guid? entityId, CancellationToken cancellationToken);
}