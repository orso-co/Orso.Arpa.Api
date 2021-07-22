using System;
using FluentValidation;

namespace Orso.Arpa.Application.Extensions
{
    public static class RuleBuilderExtensions
    {
        public static IRuleBuilder<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder)
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

        public static IRuleBuilder<T, string> ValidUri<T>(this IRuleBuilder<T, string> ruleBuilder, int maximumLength)
        {
            return ruleBuilder
                .MaximumLength(maximumLength)
                .ValidUri();
        }

        public static IRuleBuilder<T, string> Username<T>(this IRuleBuilder<T, string> ruleBuilder)
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

        public static IRuleBuilder<T, string> GeneralText<T>(this IRuleBuilder<T, string> ruleBuilder, int maximumLength)
        {
            return ruleBuilder
                .MaximumLength(maximumLength)
                .Matches(@"^[a-zA-ZáàâäãåçéèêëíìîïñóòôöõúùûüýÿæœÁÀÂÄÃÅÇÉÈÊËÍÌÎÏÑÓÒÔÖÕÚÙÛÜÝŸÆŒß$€#*%&:/\s\d\.\,\?\!\(\)\-\n\r]*$")
                .WithMessage("You entered an invalid character. Allowed characters are: a-z, A-Z, 0-9, umlauts, whitespace characters, punctuation marks, " +
                        "round braces, dashes, line breaks, slashes, hash tags, astersisks, percentage, the Euro and the Dollar sign.");
        }
    }
}
