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

            RoomEquipmentDto expectedDto = new RoomEquipmentDto {
                Id = roomEquipmentToModify.Id,
                Name = roomEquipmentToModify.Name,
                Description = modifyDto.Description,
                Quantity = modifyDto.Quantity,
                EquipmentId = roomEquipmentToModify.EquipmentId,
                Equipment = SelectValueDtoData.Chairs
            };

            // Act
            HttpClient client = _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff);

            HttpResponseMessage responseMessage = await client
                .PutAsync(ApiEndpoints.RoomEquipmentsController.Put(roomEquipmentToModify.Id), BuildStringContent(modifyDto));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);

            HttpResponseMessage getMessage = await client
                .GetAsync(ApiEndpoints.RoomEquipmentsController.Get(roomEquipmentToModify.Id));

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
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_performer)
                .GetAsync(ApiEndpoints.RoomEquipmentsController.Get(expectedDto.Id));

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
            HttpClient client = _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff);

            // Act
            HttpResponseMessage responseMessage = await client
                .DeleteAsync(ApiEndpoints.RoomEquipmentsController.Delete(roomEquipmentToDelete.Id));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);

            HttpResponseMessage getMessage = await client
                .GetAsync(ApiEndpoints.RoomEquipmentsController.Get(roomEquipmentToDelete.Id));

            getMessage.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
