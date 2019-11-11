using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Tests.Shared.DtoTestData;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Api.Tests.IntegrationTests
{
    [TestFixture]
    public class UsersControllerTests : IntegrationTestBase
    {
        [Test, Order(10002)]
        public async Task Should_Delete_User()
        {
            // Arrange
            User user = UserSeedData.Orsoadmin;

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_orsoadmin)
                .DeleteAsync(ApiEndpoints.UsersController.Delete(user.UserName));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test, Order(10001)]
        public async Task Should_Not_Delete_Deleted_User()
        {
            // Arrange
            User user = UserSeedData.DeletedUser;

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_orsoadmin)
                .DeleteAsync(ApiEndpoints.UsersController.Delete(user.UserName));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Test, Order(2)]
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

        [Test, Order(10000)]
        public async Task Should_Not_Delete_User_If_Current_User_Is_Not_Orsoadmin()
        {
            // Arrange
            User user = UserSeedData.Orsoadmin;

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_orsonaut)
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
                .AuthenticateWith(_orsonaut)
                .GetAsync(ApiEndpoints.UsersController.Get());

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            IEnumerable<UserDto> result = await DeserializeResponseMessageAsync<IEnumerable<UserDto>>(responseMessage);

            result.Should().BeEquivalentTo(expectedDtos);
        }
    }
}
