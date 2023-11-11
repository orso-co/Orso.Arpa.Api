using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Application.ClubApplication.Model;
using Orso.Arpa.Tests.Shared.TestSeedData;

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
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .GetAsync(ApiEndpoints.ClubController.Get());

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            ClubDto result = await DeserializeResponseMessageAsync<ClubDto>(responseMessage);
            result.Should().BeEquivalentTo(expectedDto);
        }
    }
}
