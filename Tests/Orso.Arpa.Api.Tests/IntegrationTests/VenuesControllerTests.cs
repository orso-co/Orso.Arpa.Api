using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Application.AddressApplication;
using Orso.Arpa.Application.RoomApplication;
using Orso.Arpa.Application.VenueApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Tests.Shared.DtoTestData;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;

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

        [Test, Order(2)]
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

        [Test, Order(100)]
        public async Task Should_Create_Venue()
        {
            var dto = new VenueCreateDto
            {
                UrbanDistrict = "Westend Süd",
                Address1 = "Bockenheimer Warte 33",
                Address2 = "c/o Frau Rotkohl",
                City = "Frankfurt am Main",
                State = "Hessen",
                Country = "Deutschland",
                Zip = "60325",
                AddressCommentInner = "Kommentar Adresse",
                Name = "Super Venue",
                Description = "Tolle Beschreibung des super Venues"
            };

            var expectedDto = new VenueDto
            {
                Name = "Super Venue",
                Description = "Tolle Beschreibung des super Venues",
                CreatedAt = FakeDateTime.UtcNow,
                CreatedBy = "Staff Member",
                Address = new AddressDto
                {
                    CreatedAt = FakeDateTime.UtcNow,
                    CreatedBy = "Staff Member",
                    UrbanDistrict = "Westend Süd",
                    Address1 = "Bockenheimer Warte 33",
                    Address2 = "c/o Frau Rotkohl",
                    City = "Frankfurt am Main",
                    CommentInner = "Kommentar Adresse",
                    Country = "Deutschland",
                    State = "Hessen",
                    Zip = "60325"
                }
            };

            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .PostAsync(ApiEndpoints.VenuesController.Post(), BuildStringContent(dto));

            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            VenueDto result = await DeserializeResponseMessageAsync<VenueDto>(responseMessage);
            result.Should().BeEquivalentTo(expectedDto, opt => opt.Excluding(dto => dto.Id).Excluding(dto => dto.Address).Excluding(dto => dto.AddressId));
            result.Id.Should().NotBeEmpty();
            result.Address.Should().BeEquivalentTo(expectedDto.Address, opt => opt.Excluding(dto => dto.Id));
            result.Address.Id.Should().NotBeEmpty();
            result.AddressId.Should().Be(result.Address.Id);
        }

        [Test, Order(1000)]
        public async Task Should_Modify_Venue()
        {
            Venue venue = VenueSeedData.WeiherhofSchule;

            var dto = new VenueModifyBodyDto
            {
                UrbanDistrict = "Westend Süd",
                Address1 = "Bockenheimer Warte 33",
                Address2 = "c/o Frau Rotkohl",
                City = "Frankfurt am Main",
                State = "Hessen",
                Country = "Deutschland",
                Zip = "60325",
                AddressCommentInner = "Kommentar Adresse",
                Name = "Super Venue",
                Description = "Tolle Beschreibung des super Venues",
            };

            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .PutAsync(ApiEndpoints.VenuesController
                    .Put(venue.Id), BuildStringContent(dto));

            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Test, Order(10000)]
        public async Task Should_Delete_Venue()
        {
            Venue venue = VenueSeedData.WeiherhofSchule;

            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .DeleteAsync(ApiEndpoints.VenuesController
                    .Delete(venue.Id));

            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
