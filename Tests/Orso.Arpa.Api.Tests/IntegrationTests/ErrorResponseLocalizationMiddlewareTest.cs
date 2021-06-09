using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Enums;
using Orso.Arpa.Infrastructure.Localization;

namespace Orso.Arpa.Api.Tests.IntegrationTests
{
    public class ErrorResponseLocalizationMiddlewareTest : IntegrationTestBase
    {
        [Test, Order(1)]
        public async Task Should_Localize_Default_En_GB()
        {
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient()
                .GetAsync(ApiEndpoints.AppointmentsController.Get(DateTime.Now, DateRange.Day));

            responseMessage.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

            string errorResponse = await responseMessage.Content.ReadAsStringAsync();
            ValidationProblemDetails errorMessage = JsonConvert.DeserializeObject<ValidationProblemDetails>(errorResponse);

            errorMessage.Title.Should().Be("Please try to login again");
        }

        [Test, Order(2)]
        public async Task Should_Localize_De_DE()
        {
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient()
                .GetAsync(ApiEndpoints.RolesController.Get()+"?culture=de-DE");

            responseMessage.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

            string errorResponse = await responseMessage.Content.ReadAsStringAsync();
            ValidationProblemDetails errorMessage = JsonConvert.DeserializeObject<ValidationProblemDetails>(errorResponse);

            errorMessage.Title.Should().Be("Bitte melde dich erneut an");
        }

        [Test, Order(3)]
        public async Task Should_Localize_En_US()
        {
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient()
                .GetAsync(ApiEndpoints.RolesController.Get()+"?culture=en-US");

            responseMessage.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

            string errorResponse = await responseMessage.Content.ReadAsStringAsync();
            ValidationProblemDetails errorMessage = JsonConvert.DeserializeObject<ValidationProblemDetails>(errorResponse);

            errorMessage.Title.Should().Be("Please try to login again");
        }

        [Test, Order(4)]
        public async Task Should_Localize_Accept_Language_Header_De_DE()
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get,
                ApiEndpoints.AppointmentsController.Get(DateTime.Now, DateRange.Day));
            requestMessage.Headers.Add("Accept-Language", "de-DE");
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient()
                .SendAsync(requestMessage);

            responseMessage.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

            string errorResponse = await responseMessage.Content.ReadAsStringAsync();
            ValidationProblemDetails errorMessage = JsonConvert.DeserializeObject<ValidationProblemDetails>(errorResponse);

            errorMessage.Title.Should().Be("Bitte melde dich erneut an");
        }

        [Test, Order(5)]
        public async Task Should_Localize_Accept_Language_Header_En_GB()
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get,
                ApiEndpoints.AppointmentsController.Get(DateTime.Now, DateRange.Day));
            requestMessage.Headers.Add("Accept-Language", "en-GB");
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient()
                .SendAsync(requestMessage);

            responseMessage.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

            string errorResponse = await responseMessage.Content.ReadAsStringAsync();
            ValidationProblemDetails errorMessage = JsonConvert.DeserializeObject<ValidationProblemDetails>(errorResponse);

            errorMessage.Title.Should().Be("Please try to login again");
        }

        [Test, Order(6)]
        public async Task Should_Localize_Accept_Language_Header_En_US_Defaults_To_En()
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get,
                ApiEndpoints.AppointmentsController.Get(DateTime.Now, DateRange.Day));
            requestMessage.Headers.Add("Accept-Language", "en-US");
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient()
                .SendAsync(requestMessage);

            responseMessage.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

            string errorResponse = await responseMessage.Content.ReadAsStringAsync();
            ValidationProblemDetails errorMessage = JsonConvert.DeserializeObject<ValidationProblemDetails>(errorResponse);

            errorMessage.Title.Should().Be("Please try to login again");
        }

        [Test, Order(7)]
        public async Task Should_Localize_Accept_Language_Header_Default_En()
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get,
                ApiEndpoints.AppointmentsController.Get(DateTime.Now, DateRange.Day));
            requestMessage.Headers.Add("Accept-Language", "*");
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient()
                .SendAsync(requestMessage);

            responseMessage.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

            string errorResponse = await responseMessage.Content.ReadAsStringAsync();
            ValidationProblemDetails errorMessage = JsonConvert.DeserializeObject<ValidationProblemDetails>(errorResponse);

            errorMessage.Title.Should().Be("Please try to login again");
        }

        [Test, Order(8)]
        public void Should_Return_All_Requested_LocalizedStrings()
        {
            LocalizerCache localizerCache = Substitute.For<LocalizerCache>(FormatterServices.GetUninitializedObject(typeof(ServiceCollection)));
            localizerCache.GetAllTranslations("RegionDto","de-DE").Returns(
                new List<Localization>
                {
                    new(new Guid(), "source", "quelle", "de-DE", "RegionDto"),
                    new(new Guid(), "target", "ziel", "de-DE", "RegionDto")
                }
            );
            localizerCache.GetAllTranslations("RegionDto", "de").Returns(
                new List<Localization>
                {
                    new(new Guid(), "german", "deutsch", "de", "RegionDto"),
                    new(new Guid(), "hallo", "hello", "de", "RegionDto")
                }
            );
            StringLocalizer sl = new StringLocalizer("RegionDto", "de-DE", localizerCache);

            IEnumerable<LocalizedString> lz = sl.GetAllStrings(false);
            lz.ToList().Count.Should().Be(2);

            lz = sl.GetAllStrings(true);
            lz.ToList().Count.Should().Be(4);
        }
    }

}
