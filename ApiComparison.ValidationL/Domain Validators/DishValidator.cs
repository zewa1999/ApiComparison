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
            .NotEmpty()
            .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _)).When(x => !string.IsNullOrEmpty(x.PhotoUrl));

        RuleForEach(x => x.Ingredients)
            .NotNull()
            .NotEmpty()
            .SetValidator(new IngredientValidator());
    }
}