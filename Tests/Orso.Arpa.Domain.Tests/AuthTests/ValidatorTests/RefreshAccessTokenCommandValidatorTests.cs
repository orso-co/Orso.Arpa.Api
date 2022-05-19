using System.Threading.Tasks;
using NUnit.Framework;
using Orso.Arpa.Tests.Shared.Extensions;
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
        public async Task Should_Have_Validation_Error_If_Invalid_Token_Is_Supplied([Values(null, "")] string token)
        {
            await _validator.ShouldHaveValidationErrorForExactAsync(query => query.RefreshToken, token);
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_Token_Is_Supplied()
        {
            await _validator.ShouldNotHaveValidationErrorForExactAsync(query => query.RefreshToken, "token");
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Invalid_Ip_Is_Supplied([Values(null, "")] string ip)
        {
            await _validator.ShouldHaveValidationErrorForExactAsync(query => query.RemoteIpAddress, ip);
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_Ip_Is_Supplied()
        {
            await _validator.ShouldNotHaveValidationErrorForExactAsync(query => query.RemoteIpAddress, "ip");
        }
    }
}
