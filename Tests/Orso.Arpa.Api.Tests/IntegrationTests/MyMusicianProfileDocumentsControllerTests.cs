using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Api.Tests.IntegrationTests
{
    public class MyMusicianProfileDocumentsControllerTests : IntegrationTestBase
    {
        [Test, Order(100)]
        public async Task Should_Add_Document()
        {
            // Act
            HttpResponseMessage loginResponse = await LoginUserAsync(_performer);
            HttpRequestMessage requestMessage = CreateRequestWithCookie(HttpMethod.Post, ApiEndpoints.MyMusicianProfileDocumentsController.Add(
                    MusicianProfileSeedData.PerformerMusicianProfile.Id,
                    SelectValueMappingSeedData.MusicianProfileDocumentsMappings[1].Id), loginResponse, "sessionCookie");
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient().SendAsync(requestMessage);

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Test, Order(101)]
        public async Task Should_Remove_Document()
        {
            // Act
            HttpResponseMessage loginResponse = await LoginUserAsync(_performer);
            HttpRequestMessage requestMessage = CreateRequestWithCookie(HttpMethod.Delete, ApiEndpoints.MyMusicianProfileDocumentsController.Remove(
                    MusicianProfileSeedData.PerformerMusicianProfile.Id,
                    SelectValueMappingSeedData.MusicianProfileDocumentsMappings[0].Id), loginResponse, "sessionCookie");
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient().SendAsync(requestMessage);

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Test, Order(100)]
        public async Task Should_Return_Forbidden_If_MusicianProfile_Is_Not_Assigned_To_User()
        {
            // Act
            HttpResponseMessage loginResponse = await LoginUserAsync(_performer);
            HttpRequestMessage requestMessage = CreateRequestWithCookie(HttpMethod.Post, ApiEndpoints.MyMusicianProfileDocumentsController.Add(
                    MusicianProfileSeedData.StaffMusicianProfile1.Id,
                    SelectValueMappingSeedData.MusicianProfileDocumentsMappings[1].Id), loginResponse, "sessionCookie");
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient().SendAsync(requestMessage);

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }
    }
}
