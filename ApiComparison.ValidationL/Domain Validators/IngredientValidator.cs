using ApiComparison.Domain.Entities;
using FluentValidation;

namespace ApiComparison.Contracts.Validators;

internal class IngredientValidator : AbstractValidator<Ingredient>
{
    public IngredientValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull()
            .Matches("[A-Za-z]");

        RuleFor(x => x.Quantity)
            .NotNull()
            .NotEmpty();

        RuleForEach(x => x.Dishes)
            .NotNull()
            .SetValidator(x => new DishValidator());
    }
}