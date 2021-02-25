using System;
using FluentValidation;
using Microsoft.Extensions.Localization;
using Orso.Arpa.Application.Resources.Cultures;
using Microsoft.Extensions.Localization;

namespace Orso.Arpa.Application.Extensions
{
    public static class RuleBuilderExtensions
    {
        public static IRuleBuilder<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder, IStringLocalizer localizer)
        {
            return ruleBuilder
                    .NotEmpty()
                    .MinimumLength(6).WithMessage(localizer["Password must be at least 6 characters"])
                    .Matches("[A-Z]").WithMessage(localizer["Password must contain at least one uppercase letter"])
                    .Matches("[a-z]").WithMessage(localizer["Password must contain at least one lowercase letter"])
                    .Matches("[0-9]").WithMessage(localizer["Password must contain at least one digit"])
                    .Matches("[^a-zA-Z0-9]").WithMessage(localizer["Password must contain at least one special character"]);
        }

        public static IRuleBuilder<T, string> ValidUri<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty()
                .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _));
        }

        public static IRuleBuilder<T, string> ValidUri<T>(this IRuleBuilder<T, string> ruleBuilder, int maximumLength)
        {
            return ruleBuilder
                .MaximumLength(maximumLength)
                .ValidUri();
        }

        public static IRuleBuilder<T, string> Username<T>(this IRuleBuilder<T, string> ruleBuilder, IStringLocalizer localizer)
        {
            return ruleBuilder
                .MaximumLength(256)
                .MinimumLength(4)
                .Matches("^[a-zA-Z0-9]*$")
                .WithMessage("Username may only contain alphanumeric characters");
        }

        /// <summary>
        /// valid SEPA characters in DFÜ Abkommen (Deutsche Kreditwirtschaft)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ruleBuilder"></param>
        public static IRuleBuilder<T, string> Sepa<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .Matches(@"^[a-zA-Z0-9\/\-\?:()\.,\+ ]*$")
                .WithMessage("A valid SEPA string may only contain alphanumeric, space and the following speacial characters: / ? : ( ) . , ' + -");
        }

        /// <summary>
        /// Five-Star rating 1 (lowest rating) .. 5 (topmost rating), 0 = rating not yet specified
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ruleBuilder"></param>
        public static IRuleBuilder<T, byte> FiveStarRating<T>(this IRuleBuilder<T, byte> ruleBuilder)
        {
            return ruleBuilder
                .InclusiveBetween<T, byte>(0, 5);
        }

        /// <summary>
        /// valid SEPA characters in DFÜ Abkommen (Deutsche Kreditwirtschaft)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ruleBuilder"></param>
        public static IRuleBuilder<T, string> Sepa<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .Matches(@"^[a-zA-Z0-9\/\-\?:()\.,\+ ]*$")
                .WithMessage("A valid SEPA string may only contain alphanumeric, space and the following speacial characters: / ? : ( ) . , ' + -");
        }
    }
}
