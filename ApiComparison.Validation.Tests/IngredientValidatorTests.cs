using ApiComparison.Contracts.Validators;
using ApiComparison.Domain.Entities;
using FluentValidation.TestHelper;
using Xunit;

namespace ApiComparison.Validation.Tests;

public class IngredientValidatorTests
{
    [Fact]
    public void IngredientValidator_AllRulesPass()
    {
        var ingredient = new Ingredient
        {
            Name = "TestIngredient",
            Quantity = 1.0,
            UnitOfMeasure = "TestUnit",
            Dishes = new List<Dish> { new Dish { Description = "Delicious", PhotoUrl = "http://example.com", Name = "Test Dish" } }
        };

        var validator = new IngredientValidator();
        var result = validator.TestValidate(ingredient);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void IngredientValidator_AllRulesFail()
    {
        var ingredient = new Ingredient
        {
            Name = null!,
            Quantity = 0,
            UnitOfMeasure = null!,
            Dishes = null!
        };

        var validator = new IngredientValidator();
        var result = validator.TestValidate(ingredient);
        result.ShouldHaveAnyValidationError();
    }
}
