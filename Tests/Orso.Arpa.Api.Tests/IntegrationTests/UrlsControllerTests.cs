using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Application.UrlApplication.Model;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.DtoTestData;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Api.Tests.IntegrationTests
{
    [TestFixture]
    public class UrlControllerTests : IntegrationTestBase
    {
        [Test, Order(1)]
        public async Task Should_Get_By_Id()
        {
            // Arrange
            UrlDto expectedUrl = UrlDtoData.ArpaWebsite;
            HttpClient client = _authenticatedServer.CreateClient().AuthenticateWith(_staff);

            // Act
            HttpResponseMessage responseMessage = await client
                .GetAsync(ApiEndpoints.UrlsController.Get(expectedUrl.Id));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            UrlDto result = await DeserializeResponseMessageAsync<UrlDto>(responseMessage);
            result.Should().BeEquivalentTo(expectedUrl);
        }

        [Test, Order(100)]
        public async Task Should_Modify()
        {
            // Arrange
            UrlDto urlToModify = UrlDtoData.GoogleDe;
            var modifyDto = new UrlModifyBodyDto
            {
                Href = "http://google.de/modified",
                AnchorText = "modified anchor",
            };
            HttpClient client = _authenticatedServer.CreateClient().AuthenticateWith(_staff);

            // Act
            HttpResponseMessage responseMessage = await client
                .PutAsync(ApiEndpoints.UrlsController.Put(urlToModify.Id), BuildStringContent(modifyDto));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Test, Order(1000)]
        public async Task Should_Add_Role()
        {
            // Arrange
            UrlDto expectedDto = UrlDtoData.ArpaWebsite;
            expectedDto.Roles.Add(RoleDtoData.Performer);

            HttpClient client = _authenticatedServer.CreateClient().AuthenticateWith(_staff);

            HttpResponseMessage responseMessage = await client
                .PostAsync(ApiEndpoints.UrlsController.AddRole(expectedDto.Id, RoleDtoData.Performer.Id), null);

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            UrlDto result = await DeserializeResponseMessageAsync<UrlDto>(responseMessage);
            result.Should().BeEquivalentTo(expectedDto);
        }

        [Test, Order(10000)]
        public async Task Should_Delete()
        {
            // Arrange
            UrlDto urlToDelete = ProjectDtoData.HoorayForHollywood.Urls[0];
            HttpClient client = _authenticatedServer.CreateClient().AuthenticateWith(_staff);

            // Act: delete url from projects list of urls
            HttpResponseMessage responseMessage = await client
                .DeleteAsync(ApiEndpoints.UrlsController.Delete(urlToDelete.Id));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);

            HttpResponseMessage getResponseMessage = await client
                .GetAsync(ApiEndpoints.UrlsController.Get(urlToDelete.Id));
            getResponseMessage.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Test, Order(10001)]
        public async Task Should_Remove_Role()
        {
            // Arrange
            UrlDto expectedDto = UrlDtoData.ArpaWebsite;
            expectedDto.Roles.Clear();

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .DeleteAsync(ApiEndpoints.UrlsController.RemoveRole(
                    UrlSeedData.ArpaWebsite.Id,
                    RoleSeedData.Staff.Id));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
