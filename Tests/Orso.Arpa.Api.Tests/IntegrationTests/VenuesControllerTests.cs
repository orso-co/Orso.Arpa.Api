using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Application.AddressApplication.Model;
using Orso.Arpa.Application.RoomApplication.Model;
using Orso.Arpa.Application.VenueApplication.Model;
using Orso.Arpa.Domain.VenueDomain.Model;
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
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            IEnumerable<VenueDto> result = await DeserializeResponseMessageAsync<IEnumerable<VenueDto>>(responseMessage);
            _ = result.Should().BeEquivalentTo(VenueDtoData.Venues);
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
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            IEnumerable<RoomDto> result = await DeserializeResponseMessageAsync<IEnumerable<RoomDto>>(responseMessage);
            _ = result.Should().BeEquivalentTo(VenueDtoData.WeiherhofSchule.Rooms);
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

            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            VenueDto result = await DeserializeResponseMessageAsync<VenueDto>(responseMessage);
            _ = result.Should().BeEquivalentTo(expectedDto, opt => opt.Excluding(dto => dto.Id).Excluding(dto => dto.Address).Excluding(dto => dto.AddressId));
            _ = result.Id.Should().NotBeEmpty();
            _ = result.Address.Should().BeEquivalentTo(expectedDto.Address, opt => opt.Excluding(dto => dto.Id));
            _ = result.Address.Id.Should().NotBeEmpty();
            _ = result.AddressId.Should().Be(result.Address.Id);
        }

        [Test, Order(100)]
        public async Task Should_Not_Create_Venue_Return_Validation_Error()
        {
            var dto = new VenueCreateDto
            {
                UrbanDistrict = "Westend Süd",
                Address1 = "Bockenheimer Warte 33",
                Address2 = "c/o Frau Rotkohl",
                City = "+~#'*`?|",
                State = "Hessen",
                Country = "Deutschland",
                Zip = null,
                AddressCommentInner = "Kommentar Adresse",
                Name = "Super Venue",
                Description = "Tolle Beschreibung des super Venues"
            };

            var modelStateDictionary = new ModelStateDictionary();
            modelStateDictionary.AddModelError("Zip", "'Zip' must not be empty.");
            modelStateDictionary.AddModelError("City", "Invalid character supplied. Please use only alphanumeric and space characters or one of the following: '-./(),");
            var expectedDto = new ValidationProblemDetails(modelStateDictionary)
            {
                Type = "https://tools.ietf.org/html/rfc4918#section-11.2",
                Status = 422
            };

            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .PostAsync(ApiEndpoints.VenuesController.Post(), BuildStringContent(dto));

            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            ValidationProblemDetails result = await DeserializeResponseMessageAsync<ValidationProblemDetails>(responseMessage);
            _ = result.Should().BeEquivalentTo(expectedDto, opt => opt.Excluding(dto => dto.Extensions));
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

            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
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

            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
