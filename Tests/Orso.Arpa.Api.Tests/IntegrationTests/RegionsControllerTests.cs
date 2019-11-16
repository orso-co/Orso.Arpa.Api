using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Application.Dtos;

namespace Orso.Arpa.Api.Tests.IntegrationTests
{
    [TestFixture]
    public class RegionsControllerTests : IntegrationTestBase
    {
        [Test]
        public async Task Should_Create()
        {
            // Arrange
            var createDto = new RegionCreateDto
            {
                Name = "Hawaii"
            };

            var expectedDto = new RegionDto
            {
                Name = createDto.Name,
                CreatedBy = _orsoadmin.DisplayName,
                ModifiedAt = null,
                ModifiedBy = null,
            };

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_orsoadmin)
                .PostAsync(ApiEndpoints.RegionsController.Post(), BuildStringContent(createDto));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.Created);
            RegionDto result = await DeserializeResponseMessageAsync<RegionDto>(responseMessage);

            result.Should().BeEquivalentTo(expectedDto, opt => opt.Excluding(r => r.Id).Excluding(r => r.CreatedAt));
            result.Id.Should().NotBeEmpty();
            result.CreatedAt.Should().BeCloseTo(DateTimeOffset.UtcNow, precision: 10000);
        }
    }
}
