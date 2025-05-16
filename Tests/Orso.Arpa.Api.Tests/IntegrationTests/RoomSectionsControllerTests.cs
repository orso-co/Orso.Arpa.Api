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

            RoomSectionDto expectedDto = new RoomSectionDto {
                Id = roomSectionToModify.Id,
                Name = roomSectionToModify.Name,
                Description = modifyDto.Description,
                Quantity = modifyDto.Quantity,
                InstrumentId = roomSectionToModify.InstrumentId,
                Instrument = SectionDtoData.Piano
            };

            // Act
            HttpClient client = _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff);

            HttpResponseMessage responseMessage = await client
                .PutAsync(ApiEndpoints.RoomSectionsController.Put(roomSectionToModify.Id), BuildStringContent(modifyDto));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);

            HttpResponseMessage getMessage = await client
                .GetAsync(ApiEndpoints.RoomSectionsController.Get(roomSectionToModify.Id));

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
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_performer)
                .GetAsync(ApiEndpoints.RoomSectionsController.Get(expectedDto.Id));

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
            HttpClient client = _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff);

            // Act
            HttpResponseMessage responseMessage = await client
                .DeleteAsync(ApiEndpoints.RoomSectionsController.Delete(roomSectionToDelete.Id));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);

            HttpResponseMessage getMessage = await client
                .GetAsync(ApiEndpoints.RoomSectionsController.Get(roomSectionToDelete.Id));

            getMessage.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
