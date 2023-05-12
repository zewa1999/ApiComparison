using ApiComparison.Domain.Entities;
using ApiComparison.Application.Interfaces.BusinessServices;

namespace ApiComparison.GraphQLApi.Types;

public class UserType : ObjectType<User>
{
    protected override void Configure(IObjectTypeDescriptor<User> descriptor)
    {
        descriptor.Description("Represents a user.");

        descriptor.Field(u => u.Id)
            .Description("The unique identifier of the user.");

        descriptor.Field(u => u.FirstName)
            .Description("The first name of the user.");

        descriptor.Field(u => u.LastName)
            .Description("The last name of the user.");

        descriptor.Field(u => u.Bio)
            .Description("The bio of the user.");

        descriptor.Field(u => u.Account)
            .Description("The account associated with the user.")
            .Type<AccountType>()
            .ResolveWith<UserResolvers>(u => u.GetAccount(default!, default!, CancellationToken.None));

        descriptor.Field(u => u.Address)
            .Description("The address associated with the user.")
            .Type<AddressType>()
            .ResolveWith<UserResolvers>(u => u.GetAddress(default!, default!, CancellationToken.None));

        descriptor.Ignore(u => u.AccountId);
        descriptor.Ignore(u => u.AddressId);
    }

    private class UserResolvers
    {
        public async Task<Account> GetAccount([Parent] User user, [ScopedService] IAccountService accountService, CancellationToken cancellationToken)
        {
            var account = await accountService.GetByIdAsync(user.AccountId, cancellationToken);
            ArgumentNullException.ThrowIfNull(account);
            return account;
        }

        public async Task<Address> GetAddress([Parent] User user, [ScopedService] IAddressService addressService, CancellationToken cancellationToken)
        {
            var address = await addressService.GetByIdAsync(user.AddressId, cancellationToken);
            ArgumentNullException.ThrowIfNull(address);
            return address;
        }
    }
}