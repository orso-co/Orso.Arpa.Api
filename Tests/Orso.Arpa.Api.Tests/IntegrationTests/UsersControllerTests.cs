using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Application.UserApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Tests.Shared.DtoTestData;
using Orso.Arpa.Tests.Shared.FakeData;

namespace Orso.Arpa.Api.Tests.IntegrationTests
{
    [TestFixture]
    public class UsersControllerTests : IntegrationTestBase
    {
        [Test, Order(10002)]
        public async Task Should_Delete_User()
        {
            // Arrange
            User user = FakeUsers.Admin;

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_admin)
                .DeleteAsync(ApiEndpoints.UsersController.Delete(user.UserName));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Test, Order(10001)]
        public async Task Should_Not_Delete_Deleted_User()
        {
            // Arrange
            User user = FakeUsers.DeletedUser;

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_admin)
                .DeleteAsync(ApiEndpoints.UsersController.Delete(user.UserName));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test, Order(10000)]
        public async Task Should_Not_Delete_User_If_Current_User_Is_Not_Admin()
        {
            // Arrange
            User user = FakeUsers.Admin;

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .DeleteAsync(ApiEndpoints.UsersController.Delete(user.UserName));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Test, Order(1)]
        public async Task Should_Get_All_Users()
        {
            // Arrange
            IList<UserDto> expectedDtos = UserDtoData.Users;

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .GetAsync(ApiEndpoints.UsersController.Get());

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            IEnumerable<UserDto> result = await DeserializeResponseMessageAsync<IEnumerable<UserDto>>(responseMessage);

            result.Should().BeEquivalentTo(expectedDtos);
        }
    }
}
