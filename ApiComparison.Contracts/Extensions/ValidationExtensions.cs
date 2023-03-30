using ApiComparison.Contracts.RequestDto;
using FluentValidation;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using ValidationException = FluentValidation.ValidationException;

namespace ApiComparison.Contracts.Extensions;

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
        where T: BaseRequestDto
    {
        var res = validator.Validate(instance);
        var exceptions = new List<ArgumentException>();

        if (!res.IsValid)
        {
            var ex = new ValidationException(res.Errors);
            foreach ( var error in ex.Errors )
            {
                exceptions.Add(new ArgumentException(error.ErrorMessage));
            }
            throw new AggregateException(exceptions);
        }

    }

}
