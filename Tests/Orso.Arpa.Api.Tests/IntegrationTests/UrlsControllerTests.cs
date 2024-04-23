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

            // Act
            HttpResponseMessage loginResponse = await LoginUserAsync(_staff);
            HttpRequestMessage requestMessage = CreateRequestWithCookie(HttpMethod.Get, ApiEndpoints.UrlsController.Get(expectedUrl.Id), loginResponse, "sessionCookie");
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient().SendAsync(requestMessage);

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

            // Act
            HttpResponseMessage loginResponse = await LoginUserAsync(_staff);
            HttpRequestMessage requestMessage = CreateRequestWithCookie(HttpMethod.Put, ApiEndpoints.UrlsController.Put(urlToModify.Id), loginResponse, "sessionCookie");
            requestMessage.Content = BuildStringContent(modifyDto);
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient().SendAsync(requestMessage);

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Test, Order(1000)]
        public async Task Should_Add_Role()
        {
            // Arrange
            UrlDto expectedDto = UrlDtoData.ArpaWebsite;
            expectedDto.Roles.Add(RoleDtoData.Performer);

            HttpResponseMessage loginResponse = await LoginUserAsync(_staff);
            HttpRequestMessage requestMessage = CreateRequestWithCookie(HttpMethod.Post, ApiEndpoints.UrlsController.AddRole(expectedDto.Id, RoleDtoData.Performer.Id), loginResponse, "sessionCookie");
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient().SendAsync(requestMessage);

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

            // Act: delete url from projects list of urls
            HttpResponseMessage loginResponse = await LoginUserAsync(_staff);
            HttpRequestMessage requestMessage = CreateRequestWithCookie(HttpMethod.Delete, ApiEndpoints.UrlsController.Delete(urlToDelete.Id), loginResponse, "sessionCookie");
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient().SendAsync(requestMessage);

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);

            HttpRequestMessage requestMessage2 = CreateRequestWithCookie(HttpMethod.Delete, ApiEndpoints.UrlsController.Get(urlToDelete.Id), loginResponse, "sessionCookie");
            HttpResponseMessage getResponseMessage = await _unAuthenticatedServer
                .CreateClient().SendAsync(requestMessage2);
            getResponseMessage.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Test, Order(10001)]
        public async Task Should_Remove_Role()
        {
            // Arrange
            UrlDto expectedDto = UrlDtoData.ArpaWebsite;
            expectedDto.Roles.Clear();

            // Act
            HttpResponseMessage loginResponse = await LoginUserAsync(_staff);
            HttpRequestMessage requestMessage = CreateRequestWithCookie(HttpMethod.Delete, ApiEndpoints.UrlsController.RemoveRole(
                    UrlSeedData.ArpaWebsite.Id,
                    RoleSeedData.Staff.Id), loginResponse, "sessionCookie");
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient().SendAsync(requestMessage);

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
