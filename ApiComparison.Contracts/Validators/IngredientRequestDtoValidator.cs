using ApiComparison.Contracts.RequestDto;
using FluentValidation;

namespace ApiComparison.Contracts.Validators;

public class IngredientRequestDtoValidator : AbstractValidator<IngredientRequestDto>
{
	public IngredientRequestDtoValidator()
	{
		RuleFor(x => x.Id)
			.NotNull()
			.NotEmpty();

		RuleFor(x => x.Name)
			.NotEmpty()
			.NotNull()
			.Matches("[A-Za-z]");

		RuleFor(x => x.Quantity)
			.NotNull()
			.NotEmpty();

		RuleForEach(x => x.DishIngredientsRequestDto)
			.NotEmpty()
			.NotNull()
			.SetValidator(new DishRequestDtoValidator());

	}
}
