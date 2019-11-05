using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Application.Users;
using Orso.Arpa.Application.Users.Dtos;
using Orso.Arpa.Tests.Shared.DtoTestData;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Api.Tests.IntegrationTests
{
    [TestFixture]
    public class UsersControllerTests : IntegrationTestBase
    {
        [Test]
        public async Task Should_Delete_User()
        {
            // Arrange
            Domain.User user = UserSeedData.Orsoadmin;
            var command = new Delete.Command(user.UserName);

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_orsoadmin)
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
                .AuthenticateWith(_orsoadmin)
                .DeleteAsync(ApiEndpoints.UsersController.Delete(user.UserName));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Test]
        public async Task Should_Get_Current_User_Profile()
        {
            // Arrange
            UserProfileDto expectedDto = UserProfileDtoData.Orsianer;

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_orsianer)
                .GetAsync(ApiEndpoints.UsersController.GetProfileOfCurrentUser());

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            UserProfileDto result = await DeserializeResponseMessageAsync<UserProfileDto>(responseMessage);

            result.Should().BeEquivalentTo(expectedDto);
        }

        [Test]
        public async Task Should_Not_Delete_User_If_Current_User_Is_Not_Orsoadmin()
        {
            // Arrange
            Domain.User user = UserSeedData.Orsoadmin;
            var command = new Delete.Command(user.UserName);

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_orsonaut)
                .DeleteAsync(ApiEndpoints.UsersController.Delete(user.UserName));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }
    }
}
