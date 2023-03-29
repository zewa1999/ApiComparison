using FluentValidation;
using System.Text.RegularExpressions;

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

}
