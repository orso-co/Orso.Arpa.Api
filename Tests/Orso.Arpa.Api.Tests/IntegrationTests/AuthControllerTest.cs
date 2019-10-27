using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Application.Auth;

namespace Orso.Arpa.Api.Tests.IntegrationTests
{
    public class AuthControllerTest : IntegrationTestBase
    {
        [Test]
        public async Task Should_Login()
        {
            // Act
            Domain.User user = Seed.Egon;
            var loginQuery = new Login.Query
            {
                Email = user.Email,
                Password = "Pa$$w0rd"
            };
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
    }
}
