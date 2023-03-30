using ApiComparison.Contracts.RequestDto;
using FluentValidation;

namespace ApiComparison.Contracts.Validators;

public class UserRequestDtoValidator : AbstractValidator<UserRequestDto>
{
	public UserRequestDtoValidator()
	{
        RuleFor(u => u.Id)
            .NotEmpty()
            .NotNull();

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

        RuleFor(u => u.AccountRequestDto)
            .SetValidator(new AccountRequestDtoValidator());

        RuleFor(u => u.AddressRequestDto)
           .SetValidator(new AddressRequestDtoValidator());

    }
}