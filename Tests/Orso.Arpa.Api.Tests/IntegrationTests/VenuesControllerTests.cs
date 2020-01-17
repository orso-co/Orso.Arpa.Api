using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Tests.Shared.DtoTestData;

namespace Orso.Arpa.Api.Tests.IntegrationTests
{
    [TestFixture]
    public class VenuesControllerTests : IntegrationTestBase
    {
        [Test, Order(1)]
        public async Task Should_Get_All()
        {
            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_orsianer)
                .GetAsync(ApiEndpoints.VenuesController.Get());

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            IEnumerable<VenueDto> result = await DeserializeResponseMessageAsync<IEnumerable<VenueDto>>(responseMessage);
            result.Should().BeEquivalentTo(VenueDtoData.Venues, opt => opt
                .Excluding(dto => dto.CreatedAt)
                .Excluding(dto => dto.Address.CreatedAt)
                .Excluding(dto => dto.Rooms));
            result.First().Rooms.Count().Should().Be(1);
            result.First().Rooms.Should().BeEquivalentTo(VenueDtoData.WeiherhofSchule.Rooms, opt => opt.Excluding(r => r.CreatedAt));
        }

        [Test, Order(1)]
        public async Task Should_Get_Rooms()
        {
            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_orsianer)
                .GetAsync(ApiEndpoints.VenuesController.GetRooms(VenueDtoData.WeiherhofSchule.Id));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            IEnumerable<RoomDto> result = await DeserializeResponseMessageAsync<IEnumerable<RoomDto>>(responseMessage);
            result.Should().BeEquivalentTo(VenueDtoData.WeiherhofSchule.Rooms, opt => opt.Excluding(r => r.CreatedAt));
        }
    }
}
