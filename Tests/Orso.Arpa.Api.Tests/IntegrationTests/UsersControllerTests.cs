using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Application.UserApplication.Model;
using Orso.Arpa.Domain.UserDomain.Enums;
using Orso.Arpa.Domain.UserDomain.Model;
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
            User user = FakeUsers.UnconfirmedUser;

            // Act
            HttpResponseMessage loginResponse = await LoginUserAsync(_admin);
            HttpRequestMessage requestMessage = CreateRequestWithCookie(HttpMethod.Delete, ApiEndpoints.UsersController.Delete(user.UserName), loginResponse, "sessionCookie");
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient().SendAsync(requestMessage);

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Test, Order(10001)]
        public async Task Should_Not_Delete_Deleted_User()
        {
            // Act
            HttpResponseMessage loginResponse = await LoginUserAsync(_admin);
            HttpRequestMessage requestMessage = CreateRequestWithCookie(HttpMethod.Delete, ApiEndpoints.UsersController.Delete("deletedusername"), loginResponse, "sessionCookie");
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient().SendAsync(requestMessage);

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NotFound);
            ValidationProblemDetails errorMessage = await DeserializeResponseMessageAsync<ValidationProblemDetails>(responseMessage);
            errorMessage.Title.Should().Be("Resource not found.");
            errorMessage.Status.Should().Be(404);
            errorMessage.Errors.Should().BeEquivalentTo(new Dictionary<string, string[]>() { { "UserName", s_userNotFoundMessage } });
        }

        [Test, Order(10000)]
        public async Task Should_Not_Delete_User_If_Current_User_Is_Not_Admin()
        {
            // Arrange
            User user = FakeUsers.Admin;

            // Act
            HttpResponseMessage loginResponse = await LoginUserAsync(_staff);
            HttpRequestMessage requestMessage = CreateRequestWithCookie(HttpMethod.Delete, ApiEndpoints.UsersController.Delete(user.UserName), loginResponse, "sessionCookie");
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient().SendAsync(requestMessage);

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        private static IEnumerable<TestCaseData> s_getUserTestData
        {
            get
            {
                yield return new TestCaseData(default(UserStatus?), UserDtoData.Users);
                yield return new TestCaseData(UserStatus.AwaitingRoleAssignment, new List<UserDto> { UserDtoData.UserWithoutRole, UserDtoData.LockedOutUser });
                yield return new TestCaseData(UserStatus.AwaitingEmailConfirmation, new List<UserDto> { UserDtoData.UnconfirmedUser });
                yield return new TestCaseData(UserStatus.Active, new List<UserDto> { UserDtoData.Admin, UserDtoData.Performer, UserDtoData.Staff });
            }
        }

        private static readonly string[] s_userNotFoundMessage = ["User could not be found."];
        private static readonly string[] s_removalOfLastAdministratorNotAllowedMessage = ["The operation is not allowed because it would remove the last administrator"];

        [Test, Order(1)]
        [TestCaseSource(nameof(s_getUserTestData))]
        public async Task Should_Get_All_Users(UserStatus? userStatus, IList<UserDto> expectedDtos)
        {
            // Act
            HttpResponseMessage loginResponse = await LoginUserAsync(_staff);
            HttpRequestMessage requestMessage = CreateRequestWithCookie(HttpMethod.Get, ApiEndpoints.UsersController.Get(userStatus), loginResponse, "sessionCookie");
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient().SendAsync(requestMessage);

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            IList<UserDto> result = (await DeserializeResponseMessageAsync<IEnumerable<UserDto>>(responseMessage)).ToList();

            result.Should().BeEquivalentTo(expectedDtos);
        }

        [Test, Order(10002)]
        public async Task Should_Not_Delete_Last_Admin()
        {
            // Act
            HttpResponseMessage loginResponse = await LoginUserAsync(_admin);
            HttpRequestMessage requestMessage = CreateRequestWithCookie(HttpMethod.Delete, ApiEndpoints.UsersController.Delete("admin"), loginResponse, "sessionCookie");
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient().SendAsync(requestMessage);

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            ValidationProblemDetails errorMessage = await DeserializeResponseMessageAsync<ValidationProblemDetails>(responseMessage);
            errorMessage.Title.Should().Be("One or more validation errors occurred.");
            errorMessage.Status.Should().Be(422);
            errorMessage.Errors.Should().BeEquivalentTo(new Dictionary<string, string[]>() { { "UserName", s_removalOfLastAdministratorNotAllowedMessage } });
        }

        [Test, Order(2)]
        public async Task Should_Get_User_By_Id()
        {
            // Arrange
            UserDto expectedDto = UserDtoData.LockedOutUser;

            // Act
            HttpResponseMessage loginResponse = await LoginUserAsync(_staff);
            HttpRequestMessage requestMessage = CreateRequestWithCookie(HttpMethod.Get, ApiEndpoints.UsersController.GetById(expectedDto.Id), loginResponse, "sessionCookie");
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient().SendAsync(requestMessage);

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            UserDto result = await DeserializeResponseMessageAsync<UserDto>(responseMessage);

            result.Should().BeEquivalentTo(expectedDto);
        }
    }
}
