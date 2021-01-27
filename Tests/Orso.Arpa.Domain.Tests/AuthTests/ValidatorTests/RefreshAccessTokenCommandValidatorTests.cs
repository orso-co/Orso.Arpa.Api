using FluentValidation.TestHelper;
using NUnit.Framework;
using static Orso.Arpa.Domain.Logic.Auth.RefreshAccessToken;

namespace Orso.Arpa.Domain.Tests.AuthTests.ValidatorTests
{
    [TestFixture]
    public class RefreshAccessTokenCommandValidatorTests
    {
        private Validator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new Validator();
        }

        [Test]
        public void Should_Have_Validation_Error_If_Invalid_Token_Is_Supplied([Values(null, "")] string token)
        {
            _validator.ShouldHaveValidationErrorFor(query => query.RefreshToken, token);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Token_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorFor(query => query.RefreshToken, "token");
        }

        [Test]
        public void Should_Have_Validation_Error_If_Invalid_Ip_Is_Supplied([Values(null, "")] string ip)
        {
            _validator.ShouldHaveValidationErrorFor(query => query.RemoteIpAddress, ip);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Ip_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorFor(query => query.RemoteIpAddress, "ip");
        }
    }
}
