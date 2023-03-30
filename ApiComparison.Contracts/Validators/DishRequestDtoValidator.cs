using ApiComparison.Contracts.RequestDto;
using FluentValidation;

namespace ApiComparison.Contracts.Validators;

public class DishRequestDtoValidator : AbstractValidator<DishRequestDto>
{
    public DishRequestDtoValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.Description)
            .MaximumLength(120)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.PhotoUrl)
            .MaximumLength(120)
            .NotNull()
            .NotEmpty();

        RuleForEach(x => x.DishIngredientsRequestDto)
            .NotNull()
            .NotEmpty()
            .SetValidator(new IngredientRequestDtoValidator());
    }
}
