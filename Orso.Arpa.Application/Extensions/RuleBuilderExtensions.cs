using System;
using FluentValidation;
using Microsoft.Extensions.Localization;
using Orso.Arpa.Application.Resources.Cultures;

namespace Orso.Arpa.Application.Extensions
{
    public static class RuleBuilderExtensions
    {
        public static IRuleBuilder<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder, IStringLocalizer localizer)
        {
            return ruleBuilder
                    .NotEmpty()
                    .MinimumLength(6).WithMessage("Password must be at least 6 characters")
                    .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter")
                    .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter")
                    .Matches("[0-9]").WithMessage("Password must contain at least one digit")
                    .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character");
        }

        public static IRuleBuilder<T, string> ValidUri<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty()
                .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _));
        }

        public static IRuleBuilder<T, string> Username<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .MaximumLength(256)
                .MinimumLength(4)
                .Matches(@"^[a-zA-Z0-9]*$")
                .WithMessage("Username may only contain alphanumeric characters");
        }
    }
}
