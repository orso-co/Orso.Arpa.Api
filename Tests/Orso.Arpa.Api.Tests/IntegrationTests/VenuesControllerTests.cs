using System;
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
using Orso.Arpa.Domain.VenueDomain.Enums;
using Orso.Arpa.Domain.VenueDomain.Model;
using Orso.Arpa.Persistence.Seed;
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
            HttpResponseMessage loginResponse = await LoginUserAsync(_performer);
            HttpRequestMessage requestMessage = CreateRequestWithCookie(HttpMethod.Get, ApiEndpoints.VenuesController.Get(), loginResponse, "sessionCookie");
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient().SendAsync(requestMessage);

            // Assert
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            IEnumerable<VenueDto> result = await DeserializeResponseMessageAsync<IEnumerable<VenueDto>>(responseMessage);
            _ = result.Should().BeEquivalentTo(VenueDtoData.Venues);
        }

        [Test, Order(2)]
        public async Task Should_Get_Rooms()
        {
            // Act
            HttpResponseMessage loginResponse = await LoginUserAsync(_performer);
            HttpRequestMessage requestMessage = CreateRequestWithCookie(HttpMethod.Get, ApiEndpoints.VenuesController.GetRooms(VenueDtoData.WeiherhofSchule.Id), loginResponse, "sessionCookie");
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient().SendAsync(requestMessage);

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

            HttpResponseMessage loginResponse = await LoginUserAsync(_staff);
            HttpRequestMessage requestMessage = CreateRequestWithCookie(HttpMethod.Post, ApiEndpoints.VenuesController.Post(), loginResponse, "sessionCookie");
            requestMessage.Content = BuildStringContent(dto);
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient().SendAsync(requestMessage);

            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            VenueDto result = await DeserializeResponseMessageAsync<VenueDto>(responseMessage);
            _ = result.Should().BeEquivalentTo(expectedDto, opt => opt.Excluding(dto => dto.Id).Excluding(dto => dto.Address).Excluding(dto => dto.AddressId));
            _ = result.Id.Should().NotBeEmpty();
            _ = result.Address.Should().BeEquivalentTo(expectedDto.Address, opt => opt.Excluding(dto => dto.Id));
            _ = result.Address.Id.Should().NotBeEmpty();
            _ = result.AddressId.Should().Be(result.Address.Id);
        }

        [Test, Order(101)]
        public async Task Should_Add_Room()
        {
            var createDto = new RoomCreateBodyDto
            {
                Name = "Super Room",
                Building = "Hauptgebäude",
                Floor = "1. OG",
                CeilingHeight = CeilingHeight.High,
                CapacityId = SelectValueMappingSeedData.RoomCapacityMappings[0].Id
            };

            var expectedDto = new RoomDto
            {
                Name = "Super Room",
                Floor = "1. OG",
                Building = "Hauptgebäude",
                CeilingHeight = CeilingHeight.High,
                Capacity = SelectValueDtoData.VoiceRehearsal,
                CreatedAt = FakeDateTime.UtcNow,
                CreatedBy = "Staff Member",
            };
            Venue venue = VenueSeedData.WeiherhofSchule;

            HttpResponseMessage loginResponse = await LoginUserAsync(_staff);
            HttpRequestMessage requestMessage = CreateRequestWithCookie(HttpMethod.Post, ApiEndpoints.VenuesController.GetRooms(venue.Id), loginResponse, "sessionCookie");
            requestMessage.Content = BuildStringContent(createDto);
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient().SendAsync(requestMessage);

            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.Created);
            RoomDto result = await DeserializeResponseMessageAsync<RoomDto>(responseMessage);
            _ = result.Should().BeEquivalentTo(expectedDto, opt => opt.Excluding(dto => dto.Id));
            _ = result.Id.Should().NotBeEmpty();
            responseMessage.Headers.Location.AbsolutePath.Should().Be($"/{ApiEndpoints.RoomsController.Get(result.Id)}");
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

            HttpResponseMessage loginResponse = await LoginUserAsync(_staff);
            HttpRequestMessage requestMessage = CreateRequestWithCookie(HttpMethod.Post, ApiEndpoints.VenuesController.Post(), loginResponse, "sessionCookie");
            requestMessage.Content = BuildStringContent(dto);
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient().SendAsync(requestMessage);

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

            HttpResponseMessage loginResponse = await LoginUserAsync(_staff);
            HttpRequestMessage requestMessage = CreateRequestWithCookie(HttpMethod.Put, ApiEndpoints.VenuesController
                    .Put(venue.Id), loginResponse, "sessionCookie");
            requestMessage.Content = BuildStringContent(dto);
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient().SendAsync(requestMessage);

            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Test, Order(10000)]
        public async Task Should_Delete_Venue()
        {
            Venue venue = VenueSeedData.WeiherhofSchule;

            HttpResponseMessage loginResponse = await LoginUserAsync(_staff);
            HttpRequestMessage requestMessage = CreateRequestWithCookie(HttpMethod.Delete, ApiEndpoints.VenuesController
                    .Delete(venue.Id), loginResponse, "sessionCookie");
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient().SendAsync(requestMessage);

            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
