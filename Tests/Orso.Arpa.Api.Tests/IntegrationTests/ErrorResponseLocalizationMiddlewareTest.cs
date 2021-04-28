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
        [Test]
        public async Task Should_Localize_Default()
        {
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient()
                .GetAsync(ApiEndpoints.AppointmentsController.Get(DateTime.Now, DateRange.Day));

            responseMessage.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

            string errorResponse = await responseMessage.Content.ReadAsStringAsync();
            ErrorMessage errorMessage = JsonConvert.DeserializeObject<ErrorMessage>(errorResponse);

            errorMessage.Description.Should().Be("Please try to login again");
        }

        [Test]
        public async Task Should_Localize_De()
        {
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient()
                .GetAsync(ApiEndpoints.RolesController.Get()+"?culture=de-DE");

            responseMessage.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

            string errorResponse = await responseMessage.Content.ReadAsStringAsync();
            ErrorMessage errorMessage = JsonConvert.DeserializeObject<ErrorMessage>(errorResponse);

            errorMessage.Description.Should().Be("Bitte melde dich erneut an");
        }

        [Test]
        public async Task Should_Localize_En()
        {
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient()
                .GetAsync(ApiEndpoints.RolesController.Get()+"?culture=en-US");

            responseMessage.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

            string errorResponse = await responseMessage.Content.ReadAsStringAsync();
            ErrorMessage errorMessage = JsonConvert.DeserializeObject<ErrorMessage>(errorResponse);

            errorMessage.Description.Should().Be("Please try to login again");
        }

        [Test]
        public async Task Should_Localize_Cookie_De()
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get,
                ApiEndpoints.AppointmentsController.Get(DateTime.Now, DateRange.Day));
            requestMessage.Headers.Add("Cookie", "Culture=c%3Dde-DE%7Cuic%3Dde-DE");
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient()
                .SendAsync(requestMessage);

            responseMessage.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

            string errorResponse = await responseMessage.Content.ReadAsStringAsync();
            ErrorMessage errorMessage = JsonConvert.DeserializeObject<ErrorMessage>(errorResponse);

            errorMessage.Description.Should().Be("Bitte melde dich erneut an");
        }

        [Test]
        public async Task Should_Localize_Cookie_En()
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get,
                ApiEndpoints.AppointmentsController.Get(DateTime.Now, DateRange.Day));
            requestMessage.Headers.Add("Cookie", "Culture=c%3Den-EN%7Cuic%3Den-EN");
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient()
                .SendAsync(requestMessage);

            responseMessage.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

            string errorResponse = await responseMessage.Content.ReadAsStringAsync();
            ErrorMessage errorMessage = JsonConvert.DeserializeObject<ErrorMessage>(errorResponse);

            errorMessage.Description.Should().Be("Please try to login again");
        }

        [Test]
        public async Task Should_Localize_Cookie_Default()
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get,
                ApiEndpoints.AppointmentsController.Get(DateTime.Now, DateRange.Day));
            requestMessage.Headers.Add("Cookie", "");
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
