using ApiComparison.Contracts.Validators;
using ApiComparison.Domain.Entities;
using FluentValidation.TestHelper;
using Xunit;

namespace ApiComparison.Validation.Tests;

public class UserValidatorTests
{
    [Fact]
    public void UserValidator_AllRulesPass()
    {
        var user = new User
        {
            FirstName = "TestFirstName",
            LastName = "TestLastName",
            Bio = "TestBio",
            Account = new Account { Email = "test@email.com", Username = "username", Password = "Password1@" },
            Address = new Address { Street = "StreetName", City = "CityName", StreetNumber = "1" }
        };

        var validator = new UserValidator();
        var result = validator.TestValidate(user);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void UserValidator_AllRulesFail()
    {
        var user = new User
        {
            FirstName = null!,
            LastName = null!,
            Bio = null!,
            Account = null!,
            Address = null!
        };

        var validator = new UserValidator();
        var result = validator.TestValidate(user);
        result.ShouldHaveAnyValidationError();
    }
}
