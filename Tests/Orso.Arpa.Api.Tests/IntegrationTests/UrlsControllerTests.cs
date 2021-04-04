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
            UrlDto url = UrlDtoData.Google;
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
            //// Arrange
            //Project project = ProjectSeedData.HoorayForHollywood;
            //UrlDto url = UrlDtoData.GoogleDe;
            //HttpClient client = _authenticatedServer.CreateClient().AuthenticateWith(_staff);

            //// Act: take first url of project and replace with new contents
            //HttpResponseMessage responseMessage = await client
            //    .PutAsync(ApiEndpoints.UrlController.PutUrl(project.Id, project.Urls[0].Id), BuildStringContent(url));

            //// Assert
            //responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);

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
            //// Arrange
            //Project project = ProjectSeedData.HoorayForHollywood;
            //Url url = project.Urls[0];
            //HttpClient client = _authenticatedServer.CreateClient().AuthenticateWith(_staff);

            //// Act: delete url from projects list of urls
            //HttpResponseMessage responseMessage = await client
            //    .DeleteAsync(ApiEndpoints.UrlController.DeleteUrl(project.Id, url.Id));

            //// Assert
            //responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);

            //// Act: get project to validate that url is no longer part of the list of urls
            //responseMessage = await client
            //    .GetAsync(ApiEndpoints.UrlController.Get(project.Id));

            //// Assert
            //responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            //ProjectDto result = await DeserializeResponseMessageAsync<ProjectDto>(responseMessage);

            ////TODO prüfen, dass die URL jetzt vom Projekt gelöscht wurde
        }
    }
}
