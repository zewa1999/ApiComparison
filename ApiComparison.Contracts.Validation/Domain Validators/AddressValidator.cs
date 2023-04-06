using ApiComparison.Domain.Entities;
using FluentValidation;

namespace ApiComparison.Contracts.Validators;

public class AddressValidator : AbstractValidator<Address>
{
    public AddressValidator()
    {
        RuleFor(a => a.Street)
            .NotEmpty()
            .NotNull()
            .MaximumLength(30);

        RuleFor(a => a.City)
            .NotEmpty()
            .NotNull()
            .MaximumLength(30);
    }
}