using ApiComparison.Domain.Entities;

namespace ApiComparison.GraphQLApi.Types;

public class AccountType : ObjectType<Account>
{
    protected override void Configure(IObjectTypeDescriptor<Account> descriptor)
    {
        descriptor.Description("Represents an account from the application.");

        descriptor
            .Field(a => a.Id)
            .Description("Represents the unique identifier for the account.");

        descriptor
            .Field(a => a.Username)
            .Description("Represents the username of the account.");

        descriptor
            .Field(a => a.Password)
            .Description("Represents the password of the account.")
            .Ignore();

        descriptor.Field(a => a.Email)
            .Description("Represents the email of the account.");

        descriptor.Field(a => a.CreatedAt)
            .Description("Represents the UTC time when the account was created.");

        descriptor
            .Field(a => a.LastUpdatedAt)
            .Description("Represents the UTC time when the account was last updated at.");
    }
}