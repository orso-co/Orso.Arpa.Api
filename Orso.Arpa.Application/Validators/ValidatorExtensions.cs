using FluentValidation;

namespace Orso.Arpa.Application.Validators
{
    public static class ValidatorExtensions
    {
        public static IRuleBuilder<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                    .NotEmpty()
                    .MinimumLength(6).WithMessage("Password must be at least 6 characters")
                    .Matches("[A-Z]").WithMessage("Password must contain one uppercase letter")
                    .Matches("[a-z]").WithMessage("Password must have at least one lowercase character")
                    .Matches("[0-9]").WithMessage("Password must contain a nunber")
                    .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain non alphanumeric");
        }
    }
}
