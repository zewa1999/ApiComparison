using ApiComparison.Contracts.Dto;
using FluentValidation;

namespace ApiComparison.Contracts.Validators;

public class BaseDtoValidator : AbstractValidator<BaseDto>
{
	public BaseDtoValidator()
	{
		RuleFor(b => b.Id)
			.NotEmpty()
			.NotNull();
	}
}
