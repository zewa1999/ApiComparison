using ApiComparison.Contracts.Validators;
using ApiComparison.Domain.Entities;
using FluentValidation.TestHelper;
using Xunit;

namespace ApiComparison.Validation.Tests;

public class DishValidatorTests
{
    [Fact]
    public void DishValidator_AllRulesPass()
    {
        var dish = new Dish
        {
            Description = "Delicious",
            PhotoUrl = "http://example.com",
            Ingredients = new List<Ingredient> { new Ingredient { Name = "Test Ingredient", Quantity = 1.0, UnitOfMeasure = "Test Unit" } },
            Name = "Test Dish"
        };

        var validator = new DishValidator();
        var result = validator.TestValidate(dish);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void DishValidator_AllRulesFail()
    {
        var dish = new Dish
        {
            Description = null!,
            PhotoUrl = null!,
            Ingredients = null!,
            Name = null!
        };

        var validator = new DishValidator();
        var result = validator.TestValidate(dish);
        result.ShouldHaveAnyValidationError();
    }
}
