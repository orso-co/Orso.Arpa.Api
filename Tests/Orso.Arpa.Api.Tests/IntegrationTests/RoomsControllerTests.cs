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
        [Test]
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

        [Test]
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
