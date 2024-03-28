using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Application.RegionApplication.Model;
using Orso.Arpa.Domain.RegionDomain.Enums;
using Orso.Arpa.Domain.RegionDomain.Model;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.DtoTestData;
using Orso.Arpa.Tests.Shared.FakeData;

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
                CreatedAt = FakeDateTime.UtcNow,
                ModifiedAt = null,
                ModifiedBy = null,
            };

            // Act
            HttpResponseMessage loginResponse = await LoginUserAsync(_staff);
            HttpRequestMessage requestMessage = CreateRequestWithCookie(HttpMethod.Post, ApiEndpoints.RegionsController.Post(), loginResponse, "sessionCookie");
            requestMessage.Content = BuildStringContent(createDto);
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient().SendAsync(requestMessage);

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.Created);
            RegionDto result = await DeserializeResponseMessageAsync<RegionDto>(responseMessage);

            result.Should().BeEquivalentTo(expectedDto, opt => opt
                .Excluding(dto => dto.Id));
            result.Id.Should().NotBeEmpty();
            responseMessage.Headers.Location.AbsolutePath.Should().Be($"/{ApiEndpoints.RegionsController.Get(result.Id)}");
        }

        [Test]
        public async Task Should_Modify()
        {
            // Arrange
            Region regionToModify = RegionSeedData.Berlin;
            var modifyDto = new RegionModifyBodyDto
            {
                Name = "Honolulu"
            };

            var expectedDto = new RegionDto
            {
                Id = regionToModify.Id,
                Name = modifyDto.Name,
                CreatedBy = regionToModify.CreatedBy,
                CreatedAt = null,
                ModifiedAt = FakeDateTime.UtcNow,
                ModifiedBy = _staff.DisplayName,
            };

            // Act
            HttpResponseMessage loginResponse = await LoginUserAsync(_staff);
            HttpRequestMessage requestMessage = CreateRequestWithCookie(HttpMethod.Put, ApiEndpoints.RegionsController.Put(regionToModify.Id), loginResponse, "sessionCookie");
            requestMessage.Content = BuildStringContent(modifyDto);
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient().SendAsync(requestMessage);

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);

            HttpRequestMessage requestMessage2 = CreateRequestWithCookie(HttpMethod.Get, ApiEndpoints.RegionsController.Get(regionToModify.Id), loginResponse, "sessionCookie");
            requestMessage.Content = BuildStringContent(modifyDto);
            HttpResponseMessage getMessage = await _unAuthenticatedServer
                .CreateClient().SendAsync(requestMessage2);

            getMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            RegionDto result = await DeserializeResponseMessageAsync<RegionDto>(getMessage);

            result.Should().BeEquivalentTo(expectedDto);
        }

        private static IEnumerable<TestCaseData> _regionTestData
        {
            get
            {
                yield return new TestCaseData(RegionPreferenceType.None, RegionDtoData.Regions);
                yield return new TestCaseData(RegionPreferenceType.Performance, RegionDtoData.RegionsForPerformance);
                yield return new TestCaseData(RegionPreferenceType.Rehearsal, RegionDtoData.RegionsForRehearsal);
            }
        }

        [Test, Order(1)]
        [TestCaseSource(nameof(_regionTestData))]
        public async Task Should_Get_All(RegionPreferenceType regionPreferenceType, IList<RegionDto> expectedResult)
        {
            // Act
            HttpResponseMessage loginResponse = await LoginUserAsync(_performer);
            HttpRequestMessage requestMessage = CreateRequestWithCookie(HttpMethod.Get, ApiEndpoints.RegionsController.Get(regionPreferenceType), loginResponse, "sessionCookie");
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient().SendAsync(requestMessage);

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            IEnumerable<RegionDto> result = await DeserializeResponseMessageAsync<IEnumerable<RegionDto>>(responseMessage);
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Test, Order(2)]
        public async Task Should_Get_ById()
        {
            // Arrange
            RegionDto expectedDto = RegionDtoData.Freiburg;

            // Act
            HttpResponseMessage loginResponse = await LoginUserAsync(_performer);
            HttpRequestMessage requestMessage = CreateRequestWithCookie(HttpMethod.Get, ApiEndpoints.RegionsController.Get(expectedDto.Id), loginResponse, "sessionCookie");
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient().SendAsync(requestMessage);

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            RegionDto result = await DeserializeResponseMessageAsync<RegionDto>(responseMessage);
            result.Should().BeEquivalentTo(expectedDto);
        }

        [Test]
        public async Task Should_Delete()
        {
            // Arrange
            Region regionToDelete = RegionSeedData.StuttgartCity;

            // Act
            HttpResponseMessage loginResponse = await LoginUserAsync(_staff);
            HttpRequestMessage requestMessage = CreateRequestWithCookie(HttpMethod.Delete, ApiEndpoints.RegionsController.Delete(regionToDelete.Id), loginResponse, "sessionCookie");
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient().SendAsync(requestMessage);

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
