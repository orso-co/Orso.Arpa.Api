using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Application.RoomApplication;
using Orso.Arpa.Application.VenueApplication;
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
                .AuthenticateWith(_performer)
                .GetAsync(ApiEndpoints.VenuesController.Get());

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            IEnumerable<VenueDto> result = await DeserializeResponseMessageAsync<IEnumerable<VenueDto>>(responseMessage);
            result.Should().BeEquivalentTo(VenueDtoData.Venues);
        }

        [Test, Order(1)]
        public async Task Should_Get_Rooms()
        {
            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_performer)
                .GetAsync(ApiEndpoints.VenuesController.GetRooms(VenueDtoData.WeiherhofSchule.Id));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            IEnumerable<RoomDto> result = await DeserializeResponseMessageAsync<IEnumerable<RoomDto>>(responseMessage);
            result.Should().BeEquivalentTo(VenueDtoData.WeiherhofSchule.Rooms);
        }
    }
}
