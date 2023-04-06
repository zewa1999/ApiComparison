using ApiComparison.Domain.Entities;
using FluentValidation;
using System.Text.RegularExpressions;
using ValidationException = FluentValidation.ValidationException;

namespace ApiComparison.Validation.Extensions;

public static class ValidationExtensions
{
    public static IRuleBuilderOptions<T, string> StringMustMatchSpecialCredentialsConditions<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty()
            .NotNull()
            .Must(x => Regex.Match(x, "[A-Za-z0-9]").Success)
            .WithMessage("The {PropertyName} should contain an uppercase, a lowercase and a number.");
    }

    public static void ValidateAndThrowAggregateException<T>(this IValidator<T> validator, T instance)
        where T : BaseEntity
    {
        var res = validator.Validate(instance);
        var exceptions = new List<ArgumentException>();

        if (!res.IsValid)
        {
            var ex = new ValidationException(res.Errors);
            foreach (var error in ex.Errors)
            {
                exceptions.Add(new ArgumentException(error.ErrorMessage));
            }
            throw new AggregateException(exceptions);
        }
    }
}