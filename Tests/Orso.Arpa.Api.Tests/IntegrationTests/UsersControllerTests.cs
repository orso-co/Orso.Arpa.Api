using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
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
            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_admin)
                .DeleteAsync(ApiEndpoints.UsersController.Delete("deletedusername"));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NotFound);
            ValidationProblemDetails errorMessage = await DeserializeResponseMessageAsync<ValidationProblemDetails>(responseMessage);
            errorMessage.Title.Should().Be("Resource not found.");
            errorMessage.Status.Should().Be(404);
            errorMessage.Errors.Should().BeEquivalentTo(new Dictionary<string, string[]>() { { "UserName", new[] { "User could not be found." } } });

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
            IList<UserDto> result = (await DeserializeResponseMessageAsync<IEnumerable<UserDto>>(responseMessage)).ToList();

            result.Should().BeEquivalentTo(expectedDtos);
        }

        [Test, Order(2)]
        public async Task Should_Get_User_By_Id()
        {
            // Arrange
            UserDto expectedDto = UserDtoData.LockedOutUser;

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .GetAsync(ApiEndpoints.UsersController.GetById(expectedDto.Id));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            UserDto result = (await DeserializeResponseMessageAsync<UserDto>(responseMessage));

            result.Should().BeEquivalentTo(expectedDto);
        }
    }
}
