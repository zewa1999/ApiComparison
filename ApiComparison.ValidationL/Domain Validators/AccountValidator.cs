using ApiComparison.Domain.Entities;
using ApiComparison.Validation.Extensions;
using FluentValidation;

namespace ApiComparison.Contracts.Validators;

internal class AccountValidator : AbstractValidator<Account>
{
    public AccountValidator()
    {
        RuleFor(a => a.Email)
            .EmailAddress();

        RuleFor(a => a.Username)
            .MaximumLength(20)
            .StringMustMatchSpecialCredentialsConditions();

        RuleFor(a => a.Password)
            .MinimumLength(8)
            .MaximumLength(40)
            .Matches("[a-zA-Z0-9&._$!@-]")
            .StringMustMatchSpecialCredentialsConditions();
    }
}