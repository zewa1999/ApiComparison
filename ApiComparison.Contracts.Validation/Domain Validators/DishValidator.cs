using ApiComparison.Domain.Entities;
using FluentValidation;

namespace ApiComparison.Contracts.Validators;

public class DishValidator : AbstractValidator<Dish>
{
    public DishValidator()
    {
        RuleFor(x => x.Description)
            .MaximumLength(120)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.PhotoUrl)
            .MaximumLength(120)
            .NotNull()
            .NotEmpty();

        RuleForEach(x => x.DishIngredients)
            .NotNull()
            .NotEmpty()
            .SetValidator(new IngredientValidator());
    }
}