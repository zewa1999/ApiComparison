using ApiComparison.Domain.Entities;
using ApiComparison.Application.Interfaces.BusinessServices;
using ApiComparison.EfCore.Persistence;
using Microsoft.EntityFrameworkCore;

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
            .ResolveWith<UserResolvers>(u => u.GetAccount(default!, default!));

        descriptor.Field(u => u.Address)
            .Description("The address associated with the user.")
            .Type<AddressType>()
            .ResolveWith<UserResolvers>(u => u.GetAddress(default!, default!));

        descriptor.Ignore(u => u.AccountId);
        descriptor.Ignore(u => u.AddressId);
    }

    private class UserResolvers
    {
        [UseDbContext(typeof(ApiComparisonDbContext))]
        public Account GetAccount([Parent] User user, [ScopedService] ApiComparisonDbContext context)
        {
            var account = context.Accounts
                .FirstOrDefault(account => account.Id == user.AccountId);

            ArgumentNullException.ThrowIfNull(account);
            return account;
        }
        
        [UseDbContext(typeof(ApiComparisonDbContext))]
        public Address GetAddress([Parent] User user, [ScopedService] ApiComparisonDbContext context)
        {
            var address = context.Addresses
                .FirstOrDefault(address => user.AddressId == address.Id);

            ArgumentNullException.ThrowIfNull(address);
            return address;
        }
    }
}