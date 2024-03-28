using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Application.RoomApplication.Model;
using Orso.Arpa.Tests.Shared.DtoTestData;

namespace Orso.Arpa.Api.Tests.IntegrationTests
{
    [TestFixture]
    public class RoomSectionsControllerTests : IntegrationTestBase
    {
        [Test, Order(1000)]
        public async Task Should_Modify()
        {
            // Arrange
            RoomSectionDto roomSectionToModify = RoomSectionDtoData.AulaWeiherhofSchulePiano;
            var modifyDto = new RoomSectionModifyBodyDto
            {
                Description = "Geänderte Beschreibung",
                Quantity = 2
            };

            RoomSectionDto expectedDto = new RoomSectionDto
            {
                Id = roomSectionToModify.Id,
                Name = roomSectionToModify.Name,
                Description = modifyDto.Description,
                Quantity = modifyDto.Quantity,
                InstrumentId = roomSectionToModify.InstrumentId
            };

            // Act
            HttpResponseMessage loginResponse = await LoginUserAsync(_staff);
            HttpRequestMessage requestMessage = CreateRequestWithCookie(HttpMethod.Put, ApiEndpoints.RoomSectionsController.Put(roomSectionToModify.Id), loginResponse, "sessionCookie");
            requestMessage.Content = BuildStringContent(modifyDto);
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient().SendAsync(requestMessage);

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);

            HttpRequestMessage requestMessage2 = CreateRequestWithCookie(HttpMethod.Get, ApiEndpoints.RoomSectionsController.Get(roomSectionToModify.Id), loginResponse, "sessionCookie");
            requestMessage.Content = BuildStringContent(modifyDto);
            HttpResponseMessage getMessage = await _unAuthenticatedServer
                .CreateClient().SendAsync(requestMessage2);

            getMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            RoomSectionDto result = await DeserializeResponseMessageAsync<RoomSectionDto>(getMessage);

            result.Should().BeEquivalentTo(expectedDto);
        }

        [Test, Order(1)]
        public async Task Should_Get_ById()
        {
            // Arrange
            RoomSectionDto expectedDto = RoomSectionDtoData.AulaWeiherhofSchulePiano;

            // Act
            HttpResponseMessage loginResponse = await LoginUserAsync(_performer);
            HttpRequestMessage requestMessage = CreateRequestWithCookie(HttpMethod.Get, ApiEndpoints.RoomSectionsController.Get(expectedDto.Id), loginResponse, "sessionCookie");
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient().SendAsync(requestMessage);

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            RoomSectionDto result = await DeserializeResponseMessageAsync<RoomSectionDto>(responseMessage);
            result.Should().BeEquivalentTo(expectedDto);
        }

        [Test, Order(10000)]
        public async Task Should_Delete()
        {
            // Arrange
            RoomSectionDto roomSectionToDelete = RoomSectionDtoData.AulaWeiherhofSchulePiano;

            // Act
            HttpResponseMessage loginResponse = await LoginUserAsync(_staff);
            HttpRequestMessage requestMessage = CreateRequestWithCookie(HttpMethod.Delete, ApiEndpoints.RoomSectionsController.Delete(roomSectionToDelete.Id), loginResponse, "sessionCookie");
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient().SendAsync(requestMessage);

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);

            HttpRequestMessage requestMessage2 = CreateRequestWithCookie(HttpMethod.Get, ApiEndpoints.RoomSectionsController.Get(roomSectionToDelete.Id), loginResponse, "sessionCookie");
            HttpResponseMessage getMessage = await _unAuthenticatedServer
                .CreateClient().SendAsync(requestMessage2);

            getMessage.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
