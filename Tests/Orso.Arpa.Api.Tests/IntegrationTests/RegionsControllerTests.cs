using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Application.Extensions;
using Orso.Arpa.Application.RegionApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.DtoTestData;

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
                CreatedBy = _staff.DisplayName,
                CreatedAt = DateTime.UtcNow.ToIsoString(),
                ModifiedAt = null,
                ModifiedBy = null,
            };

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .PostAsync(ApiEndpoints.RegionsController.Post(), BuildStringContent(createDto));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.Created);
            RegionDto result = await DeserializeResponseMessageAsync<RegionDto>(responseMessage);

            result.Should().BeEquivalentTo(expectedDto, opt => opt.Excluding(r => r.Id).Excluding(r => r.CreatedAt));
            result.Id.Should().NotBeEmpty();
            result.CreatedAt.Should().NotBeNullOrEmpty();
        }

        [Test]
        public async Task Should_Modify()
        {
            // Arrange
            Region regionToModify = RegionSeedData.Berlin;
            var modifyDto = new RegionModifyDto
            {
                Name = "Honolulu"
            };

            var expectedDto = new RegionDto
            {
                Id = regionToModify.Id,
                Name = modifyDto.Name,
                CreatedBy = regionToModify.CreatedBy,
                CreatedAt = regionToModify.CreatedAt.ToIsoString(),
                ModifiedAt = DateTime.UtcNow.ToIsoString(),
                ModifiedBy = _staff.DisplayName,
            };

            // Act
            HttpClient client = _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff);

            HttpResponseMessage responseMessage = await client
                .PutAsync(ApiEndpoints.RegionsController.Put(regionToModify.Id), BuildStringContent(modifyDto));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);

            HttpResponseMessage getMessage = await client
                .GetAsync(ApiEndpoints.RegionsController.Get(regionToModify.Id));

            getMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            RegionDto result = await DeserializeResponseMessageAsync<RegionDto>(getMessage);

            result.Should().BeEquivalentTo(expectedDto, opt => opt.Excluding(r => r.ModifiedAt));
            result.ModifiedAt.Should().NotBeNullOrEmpty();
        }

        [Test, Order(1)]
        public async Task Should_Get_All()
        {
            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_performer)
                .GetAsync(ApiEndpoints.RegionsController.Get());

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            IEnumerable<RegionDto> result = await DeserializeResponseMessageAsync<IEnumerable<RegionDto>>(responseMessage);
            result.Should().BeEquivalentTo(RegionDtoData.Regions);
        }

        [Test, Order(2)]
        public async Task Should_Get_ById()
        {
            // Arrange
            RegionDto expectedDto = RegionDtoData.Freiburg;

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_performer)
                .GetAsync(ApiEndpoints.RegionsController.Get(expectedDto.Id));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            RegionDto result = await DeserializeResponseMessageAsync<RegionDto>(responseMessage);
            result.Should().BeEquivalentTo(expectedDto);
        }

        [Test]
        public async Task Should_Delete()
        {
            // Arrange
            Region regionToDelete = RegionSeedData.Stuttgart;

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .DeleteAsync(ApiEndpoints.RegionsController.Delete(regionToDelete.Id));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
