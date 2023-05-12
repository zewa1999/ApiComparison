using ApiComparison.Domain.Entities;

namespace ApiComparison.Application.Interfaces.BusinessServices;

public interface IUserService : IBaseService<User>
{
    public Task<Address> GetUserAddress(Guid? entityId, CancellationToken cancellationToken);

    public Task<Account> GetUserAccount(Guid? entityId, CancellationToken cancellationToken);

    public Task<IEnumerable<Dish>> GetUserDishes(Guid? entityId, CancellationToken cancellationToken);
}