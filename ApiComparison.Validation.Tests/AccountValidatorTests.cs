using ApiComparison.Contracts.Validators;
using ApiComparison.Domain.Entities;
using FluentValidation.TestHelper;
using Xunit;

namespace ApiComparison.Validation.Tests;

public class AccountValidatorTests
{
    [Fact]
    public void AccountValidator_AllRulesPass()
    {
        var account = new Account
        {
            Email = "test@email.com",
            Username = "username",
            Password = "Password1@"
        };

        var validator = new AccountValidator();
        var result = validator.TestValidate(account);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void AccountValidator_AllRulesFail()
    {
        var account = new Account
        {
            Email = "wrong-email-format",
            Username = new string('x', 21), // 21 characters long
            Password = "short"
        };

        var validator = new AccountValidator();
        var result = validator.TestValidate(account);
        result.ShouldHaveAnyValidationError();
    }
}
