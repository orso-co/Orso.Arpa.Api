using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
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
            var loginQuery = new Login.Query
            {
                Email = "egon@test.de",
                Password = "Pa$$w0rd"
            };
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient()
                .PostAsync(ApiEndpoints.AuthController.Login(), BuildStringContent(loginQuery));

            // Assert
            var responseString = await responseMessage.Content.ReadAsStringAsync();
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);

            UserDto result = JsonConvert.DeserializeObject<UserDto>(responseString);
            result.Username.Should().Be("egon");
            result.Token.Should().NotBeNullOrEmpty();
        }
    }
}
