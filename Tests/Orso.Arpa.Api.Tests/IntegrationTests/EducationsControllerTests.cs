using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Application.EducationApplication;
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
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .GetAsync(ApiEndpoints.EducationsController.Get(expectedDto.Id));

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
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .PutAsync(ApiEndpoints.EducationsController.Put(dtoToModify.Id), BuildStringContent(modifyDto));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Test, Order(10000)]
        public async Task Should_Delete()
        {
            // Arrange
            EducationDto dtoToDelete = EducationDtoData.University;
            HttpClient client = _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff);

            // Act
            HttpResponseMessage responseMessage = await client
                .DeleteAsync(ApiEndpoints.EducationsController.Delete(dtoToDelete.Id));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);

            HttpResponseMessage getResponseMessage = await client
                .GetAsync(ApiEndpoints.EducationsController.Get(dtoToDelete.Id));
            getResponseMessage.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
