using ApiComparison.Domain.Entities;

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
    }
}