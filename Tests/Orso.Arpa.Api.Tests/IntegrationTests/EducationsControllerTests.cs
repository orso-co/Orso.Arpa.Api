using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Application.EducationApplication.Model;
using Orso.Arpa.Tests.Shared.DtoTestData;

namespace Orso.Arpa.Api.Tests.IntegrationTests
{
    [TestFixture]
    public class EducationControllerTests : IntegrationTestBase
    {
        [Test, Order(1)]
        public async Task Should_Get_By_Id()
        {
            // Arrange
            EducationDto expectedDto = EducationDtoData.University;

            // Act
            HttpResponseMessage loginResponse = await LoginUserAsync(_staff);
            HttpRequestMessage requestMessage = CreateRequestWithCookie(HttpMethod.Get, ApiEndpoints.EducationsController.Get(expectedDto.Id), loginResponse, "sessionCookie");
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient().SendAsync(requestMessage);

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            EducationDto result = await DeserializeResponseMessageAsync<EducationDto>(responseMessage);
            result.Should().BeEquivalentTo(expectedDto);
        }

        [Test, Order(100)]
        public async Task Should_Modify()
        {
            // Arrange
            EducationDto dtoToModify = EducationDtoData.University;
            var modifyDto = new EducationModifyBodyDto
            {
                TimeSpan = "2099",
                Description = "CHANGED " + dtoToModify.Description,
                Institution = dtoToModify.Institution,
                SortOrder = dtoToModify.SortOrder,
                TypeId = dtoToModify.TypeId
            };

            // Act
            HttpResponseMessage loginResponse = await LoginUserAsync(_staff);
            HttpRequestMessage requestMessage = CreateRequestWithCookie(HttpMethod.Put, ApiEndpoints.EducationsController.Put(dtoToModify.Id), loginResponse, "sessionCookie");
            requestMessage.Content = BuildStringContent(modifyDto);
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient().SendAsync(requestMessage);

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Test, Order(10000)]
        public async Task Should_Delete()
        {
            // Arrange
            EducationDto dtoToDelete = EducationDtoData.University;

            // Act
            HttpResponseMessage loginResponse = await LoginUserAsync(_staff);
            HttpRequestMessage requestMessage = CreateRequestWithCookie(HttpMethod.Delete, ApiEndpoints.EducationsController.Delete(dtoToDelete.Id), loginResponse, "sessionCookie");
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient().SendAsync(requestMessage);

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);

            HttpRequestMessage getRequestMessage = CreateRequestWithCookie(HttpMethod.Get, ApiEndpoints.EducationsController.Get(dtoToDelete.Id), loginResponse, "sessionCookie");
            HttpResponseMessage getResponseMessage = await _unAuthenticatedServer
                .CreateClient().SendAsync(getRequestMessage);
            getResponseMessage.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
