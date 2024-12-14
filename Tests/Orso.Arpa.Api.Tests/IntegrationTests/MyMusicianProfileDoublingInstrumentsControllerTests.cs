using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Application.MeApplication.Model;
using Orso.Arpa.Domain.MusicianProfileDomain.Model;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.DtoTestData;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Api.Tests.IntegrationTests
{
    public class MyMusicianProfileDoublingInstrumentsControllerTests : IntegrationTestBase
    {
        [Test, Order(100)]
        public async Task Should_Add_New_Doubling_Instrument_To_Musician_Profile()
        {
            MusicianProfile profile = MusicianProfileSeedData.PerformersDeactivatedTubaProfile;

            var dto = new MyDoublingInstrumentCreateBodyDto
            {
                AvailabilityId = SelectValueMappingSeedData.MusicianProfileSectionInstrumentAvailabilityMappings[0].Id,
                InstrumentId = SectionSeedData.FTuba.Id,
                LevelAssessmentInner = 1,
                Comment = "I LOVE playing th F-Tuba!"
            };

            var expectedDto = new MyDoublingInstrumentDto
            {
                AvailabilityId = dto.AvailabilityId,
                Comment = dto.Comment,
                CreatedAt = FakeDateTime.UtcNow,
                CreatedBy = "Per Former",
                InstrumentId = dto.InstrumentId,
                LevelAssessmentInner = dto.LevelAssessmentInner,
                Availability = SelectValueDtoData.PrivateOwnership,
                Instrument = SectionDtoData.FTuba
            };

            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_performer)
                .PostAsync(ApiEndpoints.MyMusicianProfileDoublingInstrumentsController
                    .Post(profile.Id), BuildStringContent(dto));

            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            MyDoublingInstrumentDto result = await DeserializeResponseMessageAsync<MyDoublingInstrumentDto>(responseMessage);
            result.Should().BeEquivalentTo(expectedDto, opt => opt.Excluding(dto => dto.Id));
            result.Id.Should().NotBeEmpty();
        }

        [Test, Order(1000)]
        public async Task Should_Modify_Doubling_Instrument()
        {
            MusicianProfile profile = MusicianProfileSeedData.PerformersHornMusicianProfile;

            var dto = new MyDoublingInstrumentModifyBodyDto
            {
                AvailabilityId = SelectValueMappingSeedData.MusicianProfileSectionInstrumentAvailabilityMappings[1].Id,
                Comment = "Wagner would be proud to hear that",
                LevelAssessmentInner = 5,
            };

            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_performer)
                .PutAsync(ApiEndpoints.MyMusicianProfileDoublingInstrumentsController
                    .Put(profile.Id, profile.DoublingInstruments.First().Id), BuildStringContent(dto));

            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
