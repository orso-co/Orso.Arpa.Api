using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Domain.Enums;

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
            ErrorMessage errorMessage = JsonConvert.DeserializeObject<ErrorMessage>(errorResponse);

            errorMessage.Description.Should().Be("Please try to login again");
        }

        [Test, Order(2)]
        public async Task Should_Localize_De_DE()
        {
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient()
                .GetAsync(ApiEndpoints.RolesController.Get()+"?culture=de-DE");

            responseMessage.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

            string errorResponse = await responseMessage.Content.ReadAsStringAsync();
            ErrorMessage errorMessage = JsonConvert.DeserializeObject<ErrorMessage>(errorResponse);

            errorMessage.Description.Should().Be("Bitte melde dich erneut an");
        }

        [Test, Order(3)]
        public async Task Should_Localize_En_US()
        {
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient()
                .GetAsync(ApiEndpoints.RolesController.Get()+"?culture=en-US");

            responseMessage.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

            string errorResponse = await responseMessage.Content.ReadAsStringAsync();
            ErrorMessage errorMessage = JsonConvert.DeserializeObject<ErrorMessage>(errorResponse);

            errorMessage.Description.Should().Be("Please try to login again");
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
            ErrorMessage errorMessage = JsonConvert.DeserializeObject<ErrorMessage>(errorResponse);

            errorMessage.Description.Should().Be("Bitte melde dich erneut an");
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
            ErrorMessage errorMessage = JsonConvert.DeserializeObject<ErrorMessage>(errorResponse);

            errorMessage.Description.Should().Be("Please try to login again");
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
            ErrorMessage errorMessage = JsonConvert.DeserializeObject<ErrorMessage>(errorResponse);

            errorMessage.Description.Should().Be("Please try to login again");
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
            ErrorMessage errorMessage = JsonConvert.DeserializeObject<ErrorMessage>(errorResponse);

            errorMessage.Description.Should().Be("Please try to login again");
        }
    }

    internal class ErrorMessage
    {
        public string Title { get; set; } = null;

        public string Description { get; set; } = null;

        public int Status { get; set; }

        public Dictionary<string, string[]> errors = new Dictionary<string, string[]>();

    }
}
