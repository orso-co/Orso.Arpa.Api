using System;
using FluentValidation;

namespace Orso.Arpa.Application.General.Extensions
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

        public static IRuleBuilderOptions<T, string> ValidUri<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty()
                .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _));
        }

        public static IRuleBuilderOptions<T, string> ValidUri<T>(this IRuleBuilder<T, string> ruleBuilder, int maximumLength)
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

        public static IRuleBuilderOptions<T, string> PhoneNumber<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .Matches(@"^[\+|0]{1}[0-9]{2,}[\/\-\d\s]*[0-9]{1,}$")
                .WithMessage("The supplied value is not a valid phone number");
        }

        public static IRuleBuilder<T, string> Iban<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .Matches(@"^(?:(?:IT|SM)\d{2}[A-Z]\d{22}|CY\d{2}[A-Z]\d{23}|NL\d{2}[A-Z]{4}\d{10}|LV\d{2}[A-Z]{4}\d{13}|(?:BG|BH|GB|IE)\d{2}[A-Z]{4}\d{14}|GI\d{2}[A-Z]{4}\d{15}|RO\d{2}[A-Z]{4}\d{16}|KW\d{2}[A-Z]{4}\d{22}|MT\d{2}[A-Z]{4}\d{23}|NO\d{13}|BE\d{14}|(?:DK|FI|GL|FO)\d{16}|(?:MK|SI)\d{17}|(?:AT|EE|KZ|LU|XK)\d{18}|(?:BA|HR|LI|CH|CR)\d{19}|(?:GE|DE|LT|ME|RS)\d{20}|IL\d{21}|(?:AD|CZ|ES|MD|SA|SE|SK)\d{22}|PT\d{23}|IS\d{24}|(?:FR|MR)\d{25}|(?:AL|DO|LB|PL|HU)\d{26}|(?:AZ|UA)\d{27}|(?:GR|MU)\d{28})|MC\d{22}[A-Z]\d{2}$")
                .WithMessage("The supplied value is not a valid IBAN");
        }

        public static IRuleBuilderOptions<T, string> Bic<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .Matches("^[A-Z]{6}[A-Z0-9]{2}([A-Z0-9]{3})?$")
                .WithMessage("The supplied value is not a valid BIC");
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

        public static IRuleBuilderOptions<T, string> PersonName<T>(this IRuleBuilder<T, string> ruleBuilder, int maximumLength)
        {
            return ruleBuilder
                .MaximumLength(maximumLength)
                .Matches(@"^[a-zA-ZáàâäãåçéèêëíìîïñóòôöõúùûüýÿæœÁÀÂÄÃÅÇÉÈÊËÍÌÎÏÑÓÒÔÖÕÚÙÛÜÝŸÆŒß\u0410-\u044F'\-. ]*$")
                .WithMessage("Invalid character supplied. Please use only alpabetic and space characters or one of the following: '-.");
        }

        public static IRuleBuilderOptions<T, string> PlaceName<T>(this IRuleBuilder<T, string> ruleBuilder, int maximumLength)
        {
            return ruleBuilder
                .MaximumLength(maximumLength)
                .Matches(@"^[a-zA-ZáàâäãåçéèêëíìîïñóòôöõúùûüýÿæœÁÀÂÄÃÅÇÉÈÊËÍÌÎÏÑÓÒÔÖÕÚÙÛÜÝŸÆŒß\u0410-\u044F'\-. 0-9\/(),]*$")
                .WithMessage("Invalid character supplied. Please use only alphanumeric and space characters or one of the following: '-./(),");
        }

        public static IRuleBuilderOptions<T, string> RestrictedFreeText<T>(this IRuleBuilder<T, string> ruleBuilder, int maximumLength)
        {
            return ruleBuilder
                .MaximumLength(maximumLength)
                .Matches(@"^[a-zA-ZáàâäãåçéèêëíìîïñóòôöõúùûüýÿæœÁÀÂÄÃÅÇÉÈÊËÍÌÎÏÑÓÒÔÖÕÚÙÛÜÝŸÆŒß\u0410-\u044F'\-.\s0-9\/(),$€#*%&„“"":;?!@\+]*$")
                .WithMessage("Invalid character supplied. Please use only alphanumeric and whitespace characters or one of the following: '-./(),$€#*%&„“\":;?!@+");
        }

        public static IRuleBuilderOptions<T, string> FreeText<T>(this IRuleBuilder<T, string> ruleBuilder, int maximumLength)
        {
            return ruleBuilder
                .MaximumLength(maximumLength)
                .Matches(@"^[a-zA-ZáàâäãåçéèêëíìîïñóòôöõúùûüýÿæœÁÀÂÄÃÅÇÉÈÊËÍÌÎÏÑÓÒÔÖÕÚÙÛÜÝŸÆŒß\u0410-\u044F'\-.\s0-9\/(),$€#*%&„“"":;?!@\+\^°| +~_]*$")
                .WithMessage("Invalid character supplied. Please use only alphanumeric and whitespace characters or one of the following: '-./(),$€#*%&„“\":;?!@+^°| +~_");
        }
    }
}
