using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Application.MusicianProfileApplication;
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
            Domain.Entities.MusicianProfile profile = MusicianProfileSeedData.AdminMusicianFluteProfile;

            var dto = new DoublingInstrumentCreateBodyDto
            {
                AvailabilityId = SelectValueMappingSeedData.MusicianProfileSectionInstrumentAvailabilityMappings[0].Id,
                InstrumentId = SectionSeedData.PiccoloFlute.Id,
                LevelAssessmentInner = 1,
                LevelAssessmentTeam = 2,
                Comment = "Plays like a nightingale (with cough)"
            };

            var expectedDto = new DoublingInstrumentDto
            {
                AvailabilityId = dto.AvailabilityId,
                Comment = dto.Comment,
                CreatedAt = FakeDateTime.UtcNow,
                CreatedBy = "Staff Member",
                InstrumentId = dto.InstrumentId,
                LevelAssessmentInner = dto.LevelAssessmentInner,
                LevelAssessmentTeam = dto.LevelAssessmentTeam
            };

            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .PostAsync(ApiEndpoints.MusicianProfileDoublingInstrumentsController.Post(profile.Id), BuildStringContent(dto));

            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            DoublingInstrumentDto result = await DeserializeResponseMessageAsync<DoublingInstrumentDto>(responseMessage);
            result.Should().BeEquivalentTo(expectedDto, opt => opt.Excluding(dto => dto.Id));
            result.Id.Should().NotBeEmpty();
        }
    }
}
