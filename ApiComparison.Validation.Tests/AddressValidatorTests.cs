using ApiComparison.Contracts.Validators;
using ApiComparison.Domain.Entities;
using FluentValidation.TestHelper;
using Xunit;

namespace ApiComparison.Validation.Tests;

public class AddressValidatorTests
{
    [Fact]
    public void AddressValidator_AllRulesPass()
    {
        var address = new Address
        {
            Street = "StreetName",
            City = "CityName",
            StreetNumber = "1",
        };

        var validator = new AddressValidator();
        var result = validator.TestValidate(address);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void AddressValidator_AllRulesFail()
    {
        var address = new Address
        {
            Street = null!,
            City = null!,
            StreetNumber = null!,

        };

        var validator = new AddressValidator();
        var result = validator.TestValidate(address);
        result.ShouldHaveAnyValidationError();
    }
}
