using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Application.RoomApplication.Model;
using Orso.Arpa.Domain.VenueDomain.Enums;
using Orso.Arpa.Domain.VenueDomain.Model;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.DtoTestData;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Api.Tests.IntegrationTests
{
    [TestFixture]
    public class RoomsControllerTests : IntegrationTestBase
    {
        [Test, Order(1000)]
        public async Task Should_Modify()
        {
            // Arrange
            RoomDto roomToModify = RoomDtoData.MusikraumWeiherhofSchule;
            var modifyDto = new RoomModifyBodyDto
            {
                Name = "Neuer Name",
                Building = "Neues Gebäude",
                Floor = "Neuer Stock",
                CeilingHeight = CeilingHeight.Low,
                CapacityId = SelectValueMappingSeedData.RoomCapacityMappings[1].Id
            };

            RoomDto expectedDto = new RoomDto {
                Id = roomToModify.Id,
                CreatedAt = roomToModify.CreatedAt,
                CreatedBy = roomToModify.CreatedBy,
                Name = modifyDto.Name,
                Building = modifyDto.Building,
                Floor = modifyDto.Floor,
                CeilingHeight = modifyDto.CeilingHeight,
                ModifiedAt = FakeDateTime.UtcNow,
                ModifiedBy = _staff.DisplayName,
                Capacity = SelectValueDtoData.Choir
            };

            // Act
            HttpClient client = _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff);

            HttpResponseMessage responseMessage = await client
                .PutAsync(ApiEndpoints.RoomsController.Put(roomToModify.Id), BuildStringContent(modifyDto));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);

            HttpResponseMessage getMessage = await client
                .GetAsync(ApiEndpoints.RoomsController.Get(roomToModify.Id));

            getMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            RoomDto result = await DeserializeResponseMessageAsync<RoomDto>(getMessage);

            result.Should().BeEquivalentTo(expectedDto);
        }

        [Test, Order(1001)]
        public async Task Should_Add_RoomEquipment()
        {
            var createDto = new RoomEquipmentCreateBodyDto
            {
                Description = "Neue Beschreibung",
                Quantity = 10,
                EquipmentId = SelectValueMappingSeedData.RoomEquipmentTypeMappings[0].Id
            };

            var expectedDto = new RoomEquipmentDto
            {
                Name = "WLAN",
                Description = "Neue Beschreibung",
                Quantity = 10,
                EquipmentId = SelectValueMappingSeedData.RoomEquipmentTypeMappings[0].Id,
                Equipment = SelectValueDtoData.Wlan
            };
            RoomDto room = RoomDtoData.AulaWeiherhofSchule;

            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .PostAsync(ApiEndpoints.RoomsController.AddEquipment(room.Id), BuildStringContent(createDto));

            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.Created);
            RoomEquipmentDto result = await DeserializeResponseMessageAsync<RoomEquipmentDto>(responseMessage);
            _ = result.Should().BeEquivalentTo(expectedDto, opt => opt.Excluding(dto => dto.Id));
            _ = result.Id.Should().NotBeEmpty();
            responseMessage.Headers.Location.AbsolutePath.Should().Be($"/{ApiEndpoints.RoomEquipmentsController.Get(result.Id)}");
        }

        [Test, Order(1002)]
        public async Task Should_Add_RoomSection()
        {
            var createDto = new RoomSectionCreateBodyDto
            {
                Description = "Neue Beschreibung",
                Quantity = 1,
                InstrumentId = SectionSeedData.DrumSet.Id
            };

            var expectedDto = new RoomSectionDto
            {
                Name = "Drum Set (Orchestra)",
                Description = "Neue Beschreibung",
                Quantity = 1,
                InstrumentId = SectionSeedData.DrumSet.Id,
                Instrument = SectionDtoData.DrumSet
            };
            RoomDto room = RoomDtoData.AulaWeiherhofSchule;

            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .PostAsync(ApiEndpoints.RoomsController.AddSection(room.Id), BuildStringContent(createDto));

            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.Created);
            RoomSectionDto result = await DeserializeResponseMessageAsync<RoomSectionDto>(responseMessage);
            _ = result.Should().BeEquivalentTo(expectedDto, opt => opt.Excluding(dto => dto.Id));
            _ = result.Id.Should().NotBeEmpty();
            responseMessage.Headers.Location.AbsolutePath.Should().Be($"/{ApiEndpoints.RoomSectionsController.Get(result.Id)}");
        }

        [Test, Order(1)]
        public async Task Should_Get_ById()
        {
            // Arrange
            RoomDto expectedDto = RoomDtoData.AulaWeiherhofSchule;

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_performer)
                .GetAsync(ApiEndpoints.RoomsController.Get(expectedDto.Id));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            RoomDto result = await DeserializeResponseMessageAsync<RoomDto>(responseMessage);
            result.Should().BeEquivalentTo(expectedDto);
        }

        [Test, Order(10000)]
        public async Task Should_Delete()
        {
            // Arrange
            Room roomToDelete = RoomSeedData.AulaWeiherhofSchule;
            var client = _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff);

            // Act
            HttpResponseMessage responseMessage = await client
                .DeleteAsync(ApiEndpoints.RoomsController.Delete(roomToDelete.Id));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);

            HttpResponseMessage getMessage = await client
                .GetAsync(ApiEndpoints.RoomsController.Get(roomToDelete.Id));

            getMessage.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
