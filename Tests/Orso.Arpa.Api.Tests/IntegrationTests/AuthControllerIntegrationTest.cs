using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Application.Auth;
using Orso.Arpa.Tests.Shared.SeedData;

namespace Orso.Arpa.Api.Tests.IntegrationTests
{
    public class AuthControllerIntegrationTest : IntegrationTestBase
    {
        [Test]
        public async Task Should_Login()
        {
            // Arrange
            Domain.User user = UserSeedData.Egon;
            var loginQuery = new Login.Query
            {
                Email = user.Email,
                Password = UserSeedData.ValidPassword
            };

            // Act
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient()
                .PostAsync(ApiEndpoints.AuthController.Login(), BuildStringContent(loginQuery));

            // Assert
            var token = await responseMessage.Content.ReadAsStringAsync();
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);

            token.Should().NotBeNullOrEmpty();

            JwtSecurityToken decryptedToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            decryptedToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.NameId)?.Value.Should().Be(user.UserName);
        }

        [Test]
        public async Task Should_Not_Login_Unregistered_User()
        {
            // Arrange
            var loginQuery = new Login.Query
            {
                Email = "unregistered@test.com",
                Password = UserSeedData.ValidPassword
            };

            // Act
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient()
                .PostAsync(ApiEndpoints.AuthController.Login(), BuildStringContent(loginQuery));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Test]
        public async Task Should_Not_Login_Invalid_Password()
        {
            // Arrange
            Domain.User user = UserSeedData.Egon;
            var loginQuery = new Login.Query
            {
                Email = user.Email,
                Password = "invalidPassword"
            };

            // Act
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient()
                .PostAsync(ApiEndpoints.AuthController.Login(), BuildStringContent(loginQuery));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
    }
}
