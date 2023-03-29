using ApiComparison.Contracts.Extensions;
using ApiComparison.Contracts.RequestDto;
using FluentValidation;
using System.Text.RegularExpressions;

namespace ApiComparison.Contracts.Validators;

public class AccountRequestDtoValidator : AbstractValidator<AccountRequestDto>
{
	public AccountRequestDtoValidator()
	{
		RuleFor(a => a.Id)
			.NotEmpty()
			.NotNull();
		
		RuleFor(a => a.Email)
			.EmailAddress();

		RuleFor(a => a.Username)
			.MaximumLength(20)
			.StringMustMatchSpecialCredentialsConditions();

		RuleFor(a => a.Password)
			.MinimumLength(8)
			.MaximumLength(40)
			.Must(x => Regex.Match(x, "[a-zA-Z0-9&._$!@-]").Success)
			.StringMustMatchSpecialCredentialsConditions();
    }

}
