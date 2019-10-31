using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Application.Users;
using Orso.Arpa.Tests.Shared.SeedData;

namespace Orso.Arpa.Api.Tests.IntegrationTests
{
    [TestFixture]
    public class UsersControllerTests : IntegrationTestBase
    {
        [Test]
        public async Task Should_Delete_User()
        {
            // Arrange
            Domain.User user = UserSeedData.Fritz;
            var command = new Delete.Command(user.UserName);

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(UserSeedData.Egon)
                .DeleteAsync(ApiEndpoints.UsersController.Delete(user.UserName));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task Should_Not_Delete_Deleted_User()
        {
            // Arrange
            Domain.User user = UserSeedData.DeletedUser;
            var command = new Delete.Command(user.UserName);

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(UserSeedData.Egon)
                .DeleteAsync(ApiEndpoints.UsersController.Delete(user.UserName));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
