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
    public class RoomEquipmentsControllerTests : IntegrationTestBase
    {
        [Test, Order(1000)]
        public async Task Should_Modify()
        {
            // Arrange
            RoomEquipmentDto roomEquipmentToModify = RoomEquipmentDtoData.AulaWeiherhofSchuleChairs;
            var modifyDto = new RoomEquipmentModifyBodyDto
            {
                Description = "Geänderte Beschreibung",
                Quantity = 200
            };

            RoomEquipmentDto expectedDto = new RoomEquipmentDto
            {
                Id = roomEquipmentToModify.Id,
                Name = roomEquipmentToModify.Name,
                Description = modifyDto.Description,
                Quantity = modifyDto.Quantity,
                EquipmentId = roomEquipmentToModify.EquipmentId
            };

            // Act
            HttpResponseMessage loginResponse = await LoginUserAsync(_staff);
            HttpRequestMessage requestMessage = CreateRequestWithCookie(HttpMethod.Put, ApiEndpoints.RoomEquipmentsController.Put(roomEquipmentToModify.Id), loginResponse, "sessionCookie");
            requestMessage.Content = BuildStringContent(modifyDto);
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient().SendAsync(requestMessage);

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);

            HttpRequestMessage requestMessage2 = CreateRequestWithCookie(HttpMethod.Get, ApiEndpoints.RoomEquipmentsController.Get(roomEquipmentToModify.Id), loginResponse, "sessionCookie");
            requestMessage.Content = BuildStringContent(modifyDto);
            HttpResponseMessage getMessage = await _unAuthenticatedServer
                .CreateClient().SendAsync(requestMessage2);

            getMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            RoomEquipmentDto result = await DeserializeResponseMessageAsync<RoomEquipmentDto>(getMessage);

            result.Should().BeEquivalentTo(expectedDto);
        }

        [Test, Order(1)]
        public async Task Should_Get_ById()
        {
            // Arrange
            RoomEquipmentDto expectedDto = RoomEquipmentDtoData.AulaWeiherhofSchuleChairs;

            // Act
            HttpResponseMessage loginResponse = await LoginUserAsync(_performer);
            HttpRequestMessage requestMessage = CreateRequestWithCookie(HttpMethod.Get, ApiEndpoints.RoomEquipmentsController.Get(expectedDto.Id), loginResponse, "sessionCookie");
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient().SendAsync(requestMessage);

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            RoomEquipmentDto result = await DeserializeResponseMessageAsync<RoomEquipmentDto>(responseMessage);
            result.Should().BeEquivalentTo(expectedDto);
        }

        [Test, Order(10000)]
        public async Task Should_Delete()
        {
            // Arrange
            RoomEquipmentDto roomEquipmentToDelete = RoomEquipmentDtoData.AulaWeiherhofSchuleChairs;

            // Act
            HttpResponseMessage loginResponse = await LoginUserAsync(_staff);
            HttpRequestMessage requestMessage = CreateRequestWithCookie(HttpMethod.Delete, ApiEndpoints.RoomEquipmentsController.Delete(roomEquipmentToDelete.Id), loginResponse, "sessionCookie");
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient().SendAsync(requestMessage);

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);

            HttpRequestMessage requestMessage2 = CreateRequestWithCookie(HttpMethod.Get, ApiEndpoints.RoomEquipmentsController.Get(roomEquipmentToDelete.Id), loginResponse, "sessionCookie");
            HttpResponseMessage getMessage = await _unAuthenticatedServer
                .CreateClient().SendAsync(requestMessage2);

            getMessage.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
