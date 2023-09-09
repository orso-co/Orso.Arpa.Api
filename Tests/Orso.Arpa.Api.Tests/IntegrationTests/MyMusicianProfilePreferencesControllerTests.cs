using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Application.MeApplication.Model;
using Orso.Arpa.Application.MusicianProfileApplication.Model;
using Orso.Arpa.Domain.RegionDomain.Enums;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.DtoTestData;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Api.Tests.IntegrationTests
{
    public class MyMusicianProfilePreferencesControllerTests : IntegrationTestBase
    {
        [Test, Order(100)]
        public async Task Should_Update_MusicianProfileRegionPreference()
        {
            var modifyDto = new MyRegionPreferenceModifyBodyDto
            {
                Comment = "I hate Freiburg...",
                Rating = 1
            };

            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_performer)
                .PutAsync(ApiEndpoints.MyMusicianProfileRegionPreferencesController.Put(MusicianProfileSeedData.PerformerMusicianProfile.Id, Guid.Parse("0f3de639-a287-4246-b939-24780877030e")), BuildStringContent(modifyDto));

            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Test, Order(1000)]
        public async Task Should_Add_MusicianProfileRegionPreference()
        {
            var createDto = new MyRegionPreferenceCreateBodyDto
            {
                Comment = "Comment",
                Rating = 3,
                RegionId = RegionSeedData.Jamulus.Id,
                Type = RegionPreferenceType.Rehearsal
            };
            var expectedResult = new RegionPreferenceDto
            {
                Comment = "Comment",
                CreatedAt = FakeDateTime.UtcNow,
                CreatedBy = "Per Former",
                Rating = 3,
                Region = RegionDtoData.Jamulus
            };

            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_performer)
                .PostAsync(ApiEndpoints.MyMusicianProfileRegionPreferencesController.Post(MusicianProfileSeedData.PerformerMusicianProfile.Id), BuildStringContent(createDto));

            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            RegionPreferenceDto result = await DeserializeResponseMessageAsync<RegionPreferenceDto>(responseMessage);
            result.Should().BeEquivalentTo(expectedResult, opt => opt.Excluding(d => d.Id));
            result.Id.Should().NotBeEmpty();
        }

        [Test, Order(10000)]
        public async Task Should_Delete_MusicianProfileRegionPreference()
        {
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_performer)
                .DeleteAsync(ApiEndpoints.MyMusicianProfileRegionPreferencesController.Delete(MusicianProfileSeedData.PerformerMusicianProfile.Id, Guid.Parse("0f3de639-a287-4246-b939-24780877030e")));

            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
