using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Application.ProjectApplication;
using Orso.Arpa.Application.UrlApplication;
using Orso.Arpa.Tests.Shared.DtoTestData;

namespace Orso.Arpa.Api.Tests.IntegrationTests
{
    [TestFixture]
    public class UrlControllerTests : IntegrationTestBase
    {
        [Test]
        public async Task Should_Add()
        {
            // Arrange
            ProjectDto project = ProjectDtoData.HoorayForHollywood;
            var url = new UrlCreateDto { Href = "http://google.de", AnchorText = "hallo google" };
            HttpClient client = _authenticatedServer.CreateClient().AuthenticateWith(_staff);

            // Act: add url to project
            HttpResponseMessage responseMessage = await client
                .PostAsync(ApiEndpoints.UrlsController.Post(project.Id), BuildStringContent(url));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);

            // Act: get project to validate that url has been added to list of urls
            //responseMessage = await client
            //    .GetAsync(ApiEndpoints.UrlsController.Get(project.Id));

            //// Assert
            //responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            //ProjectDto result = await DeserializeResponseMessageAsync<ProjectDto>(responseMessage);

            //TODO prüfen, dass die neue URL jetzt zum Projekt hinzugefügt wurde
        }

        [Test]
        public async Task Should_Modify()
        {
            // Arrange
            ProjectDto project = ProjectDtoData.HoorayForHollywood;
            UrlDto existing = UrlDtoData.GoogleDe;
            var url = new UrlModifyDto
            {
                Href = "http://google.com",
                AnchorText = existing.AnchorText,
                Id = existing.Id,
            };
            HttpClient client = _authenticatedServer.CreateClient().AuthenticateWith(_staff);

            // Act: take first url of project and replace with new contents
            HttpResponseMessage responseMessage = await client
                .PutAsync(ApiEndpoints.UrlsController.Put(existing.Id), BuildStringContent(url));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);

            //// Act: get project to validate that url has been changed
            //responseMessage = await client
            //    .GetAsync(ApiEndpoints.UrlController.Get(project.Id));

            //// Assert
            //responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            //ProjectDto result = await DeserializeResponseMessageAsync<ProjectDto>(responseMessage);

            ////TODO prüfen, dass die neue URL geändert wurde
        }

        [Test]
        public async Task Should_Delete()
        {
            // Arrange
            ProjectDto project = ProjectDtoData.HoorayForHollywood;
            HttpClient client = _authenticatedServer.CreateClient().AuthenticateWith(_staff);

            // Act: delete url from projects list of urls
            HttpResponseMessage responseMessage = await client
                .DeleteAsync(ApiEndpoints.UrlsController.Delete(project.Urls[0].Id));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Test]
        public async Task Should_Add_Role()
        {
            // Arrange
            UrlDto expectedDto = UrlDtoData.ArpaWebsite;
            expectedDto.Roles.Add(RoleDtoData.Performer);

            HttpClient client = _authenticatedServer.CreateClient().AuthenticateWith(_staff);

            HttpResponseMessage responseMessage = await client
                .PostAsync(ApiEndpoints.UrlsController.AddRole(expectedDto.Id, RoleDtoData.Staff.Id), null);

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            UrlDto result = await DeserializeResponseMessageAsync<UrlDto>(responseMessage);
            result.Should().BeEquivalentTo(expectedDto);
        }
        public async Task Should_Remove_Role()
        {
            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .DeleteAsync(ApiEndpoints.UrlsController.RemoveRole(
                    UrlDtoData.ArpaWebsite.Id,
                    UrlDtoData.ArpaWebsite.Roles[0].Id));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

    }
}
