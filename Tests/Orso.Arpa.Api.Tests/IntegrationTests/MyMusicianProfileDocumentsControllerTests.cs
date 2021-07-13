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
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_performer)
                .PostAsync(ApiEndpoints.MyMusicianProfileDocumentsController.Add(
                    MusicianProfileSeedData.PerformerMusicianProfile.Id,
                    SelectValueMappingSeedData.MusicianProfileDocumentsMappings[1].Id), null);

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Test, Order(101)]
        public async Task Should_Remove_Document()
        {
            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_performer)
                .DeleteAsync(ApiEndpoints.MyMusicianProfileDocumentsController.Remove(
                    MusicianProfileSeedData.PerformerMusicianProfile.Id,
                    SelectValueMappingSeedData.MusicianProfileDocumentsMappings[0].Id));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Test, Order(100)]
        public async Task Should_Return_Forbidden_If_MusicianProfile_Is_Not_Assigned_To_User()
        {
            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_performer)
                .PostAsync(ApiEndpoints.MyMusicianProfileDocumentsController.Add(
                    MusicianProfileSeedData.StaffMusicianProfile1.Id,
                    SelectValueMappingSeedData.MusicianProfileDocumentsMappings[1].Id), null);

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }
    }
}
