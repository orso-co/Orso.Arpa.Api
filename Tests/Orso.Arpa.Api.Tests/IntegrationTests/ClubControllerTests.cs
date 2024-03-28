using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Application.ClubApplication.Model;

namespace Orso.Arpa.Api.Tests.IntegrationTests
{
    [TestFixture]
    public class ClubControllerTests : IntegrationTestBase
    {
        [Test, Order(1)]
        public async Task Should_Get_Club_Data()
        {
            // Arrange
            var expectedDto = new ClubDto
            {
                Name = "ORSO – Orchestra & Choral Society Freiburg | Berlin e. V.",
                Address = "Schwarzwaldstr. 9-11, 79117 Freiburg",
                ContactEmail = "mail@orso.co",
                Phone = "+4907617073203",
                Url = new Uri("https://www.orso.co/")
            };

            // Act
            HttpResponseMessage loginResponse = await LoginUserAsync(_staff);
            HttpRequestMessage requestMessage = CreateRequestWithCookie(HttpMethod.Get, ApiEndpoints.ClubController.Get(), loginResponse, "sessionCookie");
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient().SendAsync(requestMessage);

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            ClubDto result = await DeserializeResponseMessageAsync<ClubDto>(responseMessage);
            result.Should().BeEquivalentTo(expectedDto);
        }
    }
}
