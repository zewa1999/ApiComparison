using ApiComparison.Domain.Entities;
using FluentValidation;

namespace ApiComparison.Contracts.Validators;

internal class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(u => u.FirstName)
            .NotEmpty()
            .NotNull()
            .MaximumLength(40);

        RuleFor(u => u.LastName)
            .NotEmpty()
            .NotNull()
            .MaximumLength(40);

        RuleFor(u => u.Bio)
            .NotEmpty()
            .NotNull()
            .MaximumLength(120);

        RuleFor(u => u.Account)
            .SetValidator(new AccountValidator());

        RuleFor(u => u.Address)
           .SetValidator(new AddressValidator());
    }
}