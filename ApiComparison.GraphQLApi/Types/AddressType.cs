using ApiComparison.Application.Interfaces.BusinessServices;
using ApiComparison.Domain.Entities;
using ApiComparison.Domain.Interfaces.Repositories;

namespace ApiComparison.GraphQLApi.Types;

public class AddressType : ObjectType<Address>
{
    protected override void Configure(IObjectTypeDescriptor<Address> descriptor)
    {
        descriptor.Description("Represents an address.");

        descriptor
            .Field(a => a.Id)
            .Description("Represents the unique identifier for the address.");

        descriptor.Field(a => a.Street)
            .Description("The street name.");

        descriptor.Field(a => a.StreetNumber)
            .Description("The street number.");

        descriptor.Field(a => a.City)
            .Description("The city name.");

        descriptor.Field(a => a.UserId)
            .Ignore();

        descriptor.Field(a => a.User)
            .Description("The user associated with the address.")
            .ResolveWith<AddressResolvers>(r => r.GetUser(default!, default!, CancellationToken.None));
    }
}

internal class AddressResolvers
{
    public async Task<User> GetUser([Parent] Address address, [ScopedService] IUserService userRepository, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(address.UserId, cancellationToken);
        ArgumentNullException.ThrowIfNull(user);
        return user;
    }
}