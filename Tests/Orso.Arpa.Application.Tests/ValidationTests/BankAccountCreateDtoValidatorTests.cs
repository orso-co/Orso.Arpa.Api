using FluentValidation.TestHelper;
using NUnit.Framework;
using Orso.Arpa.Application.BankAccountApplication.Model;
using Orso.Arpa.Tests.Shared.Extensions;

namespace Orso.Arpa.Application.Tests.ValidationTests
{
    [TestFixture]
    [SetUICulture("en-EN")]
    public class BankAccountCreateDtoValidatorTests
    {
        private readonly BankAccountCreateBodyDtoValidator _validator = new();

        [Test]
        public void Should_Fail_If_Iban_Is_Empty([Values("", null)] string iban)
        {
            _validator.ShouldHaveValidationErrorForExact(dto => dto.Iban, iban)
                .WithErrorMessage("'Iban' must not be empty.");
        }

        [Test]
        public void Should_Fail_If_Iban_Is_Invalid([Values("DE5150010517851475889", "DEDE500105178514758896345345", "12345678901")] string iban)
        {
            _validator.ShouldHaveValidationErrorForExact(dto => dto.Iban, iban)
                .WithErrorMessage("The supplied value is not a valid IBAN");
        }

        [Test]
        public void Should_Succeed_If_Valid_Iban_Is_Supplied([Values(
            "DE51500105178514758896",
            "FR7630066100410001057380116",
            "PL93109024029539298462937293",
            "UA607258949982463951425796634",
            "GB32ESSE40486562136016",
            "CH0209000000100013997",
            "RO75PORL7735359932844651",
            "HU18131000075779848666912277",
            "BE68844010370034",
            "AT026000000001349870",
            "LI0208800000017197386",
            "AL90208110080000001039531801",
            "DK5750510001322617",
            "EE342200221034126658",
            "FI9814283500171141",
            "IE92BOFI90001710027952",
            "IT68D0300203280000400162854",
            "LU761111000872960000",
            "MT98MMEB44093000000009027293051",
            "MC1112739000700011111000H79",
            "NL18ABNA0484869868",
            "NO5015032080119",
            "PT50003506830000000784311",
            "SM86U0322509800000000270100",
            "SE6412000000012170145230",
            "SK9311110000001057361004",
            "SI56031001001300933",
            "ES1020903200500041045040",
            "CZ4201000000195505030267")] string iban)
        {
            _validator.ShouldNotHaveValidationErrorForExact(dto => dto.Iban, iban);
        }

        [Test]
        public void Should_Fail_If_Iban_Is_Not_German_And_Bic_Is_Empty([Values("", null)] string bic)
        {
            _validator.ShouldHaveValidationErrorForExact(dto => dto.Bic, new BankAccountCreateBodyDto
            {
                Bic = bic,
                Iban = "CZ4201000000195505030267"
            }).WithErrorMessage("'Bic' must not be empty.");
        }

        [Test]
        public void Should_Succeed_If_Iban_Is_German_And_Bic_Is_Empty([Values("", null)] string bic)
        {
            _validator.ShouldNotHaveValidationErrorForExact(dto => dto.Bic, new BankAccountCreateBodyDto
            {
                Bic = bic,
                Iban = "DE51500105178514758896"
            });
        }

        [Test]
        public void Should_Fail_If_Bic_Is_Invalid([Values("ABCDEFG", "12345678901", "DE 515 001 051 785 147 588 96")] string bic)
        {
            _validator.ShouldHaveValidationErrorForExact(dto => dto.Bic, new BankAccountCreateBodyDto
            {
                Bic = bic,
                Iban = "CZ4201000000195505030267"
            }).WithErrorMessage("The supplied value is not a valid BIC");
        }

        [Test]
        public void Should_Succeed_If_Valid_Bic_Is_Supplied([Values(
            "CITIUS33",
            "RSTAALTT",
            "USALALTRDUR",
            "ASPKAT2LGRU",
            "ALFABY2X",
            "GENODE61BBB",
            "PAERCZPPMON",
            "AARBDE5W100",
            "AGBMDEMME23",
            "VOWADE2BBNK",
            "TISGDE5W")] string bic)
        {
            _validator.ShouldNotHaveValidationErrorForExact(dto => dto.Bic, new BankAccountCreateBodyDto
            {
                Bic = bic,
                Iban = "CZ4201000000195505030267"
            });
        }
    }
}
