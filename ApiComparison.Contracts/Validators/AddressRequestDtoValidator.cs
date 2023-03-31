using ApiComparison.Contracts.Dto.RequestDto;
using FluentValidation;

namespace ApiComparison.Contracts.Validators;

public class AddressRequestDtoValidator : AbstractValidator<AddressRequestDto>
{
	public AddressRequestDtoValidator()
	{
		RuleFor(a => a.Id)
			.NotEmpty()
			.NotNull();

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
