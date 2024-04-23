using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Application.DoublingInstrumentApplication.Model;
using Orso.Arpa.Domain.MusicianProfileDomain.Model;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Api.Tests.IntegrationTests
{
    public class MusicianProfileDoublingInstrumentsControllerTests : IntegrationTestBase
    {
        [Test, Order(100)]
        public async Task Should_Add_New_Doubling_Instrument_To_Musician_Profile()
        {
            MusicianProfile profile = MusicianProfileSeedData.AdminMusicianFluteProfile;

            var dto = new DoublingInstrumentCreateBodyDto
            {
                AvailabilityId = SelectValueMappingSeedData.MusicianProfileSectionInstrumentAvailabilityMappings[0].Id,
                InstrumentId = SectionSeedData.PiccoloFlute.Id,
                LevelAssessmentInner = 1,
                LevelAssessmentTeam = 2,
                Comment = " Plays like a nightingale (with cough) "
            };

            var expectedDto = new DoublingInstrumentDto
            {
                AvailabilityId = dto.AvailabilityId,
                Comment = "Plays like a nightingale (with cough)",
                CreatedAt = FakeDateTime.UtcNow,
                CreatedBy = "Staff Member",
                InstrumentId = dto.InstrumentId,
                LevelAssessmentInner = dto.LevelAssessmentInner,
                LevelAssessmentTeam = dto.LevelAssessmentTeam
            };

            HttpResponseMessage loginResponse = await LoginUserAsync(_staff);
            HttpRequestMessage requestMessage = CreateRequestWithCookie(HttpMethod.Post, ApiEndpoints.MusicianProfileDoublingInstrumentsController
                    .Post(profile.Id), loginResponse, "sessionCookie");
            requestMessage.Content = BuildStringContent(dto);
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient().SendAsync(requestMessage);

            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            DoublingInstrumentDto result = await DeserializeResponseMessageAsync<DoublingInstrumentDto>(responseMessage);
            _ = result.Should().BeEquivalentTo(expectedDto, opt => opt.Excluding(dto => dto.Id));
            _ = result.Id.Should().NotBeEmpty();
        }

        [Test, Order(1000)]
        public async Task Should_Modify_Doubling_Instrument()
        {
            MusicianProfile profile = MusicianProfileSeedData.PerformersHornMusicianProfile;

            var dto = new DoublingInstrumentModifyBodyDto
            {
                AvailabilityId = SelectValueMappingSeedData.MusicianProfileSectionInstrumentAvailabilityMappings[1].Id,
                Comment = "Wagner would be proud to hear that",
                LevelAssessmentInner = 5,
                LevelAssessmentTeam = 5
            };

            HttpResponseMessage loginResponse = await LoginUserAsync(_staff);
            HttpRequestMessage requestMessage = CreateRequestWithCookie(HttpMethod.Put, ApiEndpoints.MusicianProfileDoublingInstrumentsController
                    .Put(profile.Id, profile.DoublingInstruments.First().Id), loginResponse, "sessionCookie");
            requestMessage.Content = BuildStringContent(dto);
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient().SendAsync(requestMessage);

            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Test, Order(10000)]
        public async Task Should_Delete_Doubling_Instrument()
        {
            MusicianProfile profile = MusicianProfileSeedData.PerformersHornMusicianProfile;

            HttpResponseMessage loginResponse = await LoginUserAsync(_staff);
            HttpRequestMessage requestMessage = CreateRequestWithCookie(HttpMethod.Delete, ApiEndpoints.MusicianProfileDoublingInstrumentsController
                    .Delete(profile.Id, profile.DoublingInstruments.First().Id), loginResponse, "sessionCookie");
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient().SendAsync(requestMessage);

            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
