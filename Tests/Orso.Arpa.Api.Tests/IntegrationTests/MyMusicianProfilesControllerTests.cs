using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Application.MeApplication;
using Orso.Arpa.Application.MusicianProfileApplication;
using Orso.Arpa.Application.MyMusicianProfileApplication;
using Orso.Arpa.Application.ProjectApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.DtoTestData;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Api.Tests.IntegrationTests
{
    public class MyMusicianProfilesControllerTests : IntegrationTestBase
    {
        private static IEnumerable<TestCaseData> s_musicianProfileData
        {
            get
            {
                yield return new TestCaseData(false, new List<MyMusicianProfileDto> {
                    MyMusicianProfileDtoData.PerformerProfile,
                    MyMusicianProfileDtoData.PerformersHornMusicianProfile,
                    });
                yield return new TestCaseData(true, new List<MyMusicianProfileDto> {
                    MyMusicianProfileDtoData.PerformerProfile,
                    MyMusicianProfileDtoData.PerformersDeactivatedTubaProfile,
                    MyMusicianProfileDtoData.PerformersHornMusicianProfile
                    });
            }
        }

        [Test, Order(1)]
        [TestCaseSource(nameof(s_musicianProfileData))]
        public async Task Should_Get_My_MusicianProfiles(bool includeDeactivated, IList<MyMusicianProfileDto> expectedDtos)
        {
            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_performer)
                .GetAsync(ApiEndpoints.MyMusicianProfilesController.Get(includeDeactivated));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            IList<MyMusicianProfileDto> result = await DeserializeResponseMessageAsync<IList<MyMusicianProfileDto>>(responseMessage);
            result.Should().BeEquivalentTo(expectedDtos, opt => opt.WithStrictOrdering());
        }

        [Test, Order(2)]
        public async Task Should_Get_My_MusicianProfile_ById()
        {
            // Arrange
            MyMusicianProfileDto expectedDto = MyMusicianProfileDtoData.PerformerProfile;

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_performer)
                .GetAsync(ApiEndpoints.MyMusicianProfilesController.GetById(expectedDto.Id));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            MusicianProfileDto result = await DeserializeResponseMessageAsync<MusicianProfileDto>(responseMessage);
            result.Should().BeEquivalentTo(expectedDto);
        }

        [Test, Order(1000)]
        public async Task Should_Add_My_MusicianProfile()
        {
            // Arrange
            var createDto = new MyMusicianProfileCreateDto
            {
                InstrumentId = SectionSeedData.Clarinet.Id,
                LevelAssessmentInner = 1,
            };
            createDto.PreferredPositionsInnerIds.Add(SelectValueSectionSeedData.ClarinetCoach.Id);
            createDto.PreferredPartsInner.Add(2);
            createDto.PreferredPartsInner.Add(4);

            var createDoublingInstrumentDto = new MyDoublingInstrumentCreateDto
            {
                InstrumentId = SectionSeedData.EbClarinet.Id,
                AvailabilityId = SelectValueMappingSeedData.MusicianProfileSectionInstrumentAvailabilityMappings[0].Id,
                LevelAssessmentInner = 4,
                Comment = "my comment"
            };
            createDto.DoublingInstruments.Add(createDoublingInstrumentDto);

            var expectedDto = new MyMusicianProfileDto
            {
                InstrumentId = createDto.InstrumentId,
                LevelAssessmentInner = createDto.LevelAssessmentInner,
                CreatedBy = _performer.DisplayName,
                CreatedAt = FakeDateTime.UtcNow,
                PersonId = _performer.PersonId
            };
            expectedDto.PreferredPositionsInnerIds.Add(SelectValueSectionSeedData.ClarinetCoach.Id);
            expectedDto.PreferredPartsInner.Add(2);
            expectedDto.PreferredPartsInner.Add(4);
            expectedDto.DoublingInstruments.Add(new MyDoublingInstrumentDto
            {
                AvailabilityId = createDoublingInstrumentDto.AvailabilityId,
                Comment = createDoublingInstrumentDto.Comment,
                InstrumentId = createDoublingInstrumentDto.InstrumentId,
                CreatedAt = FakeDateTime.UtcNow,
                CreatedBy = _performer.DisplayName,
                LevelAssessmentInner = createDoublingInstrumentDto.LevelAssessmentInner
            });

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_performer)
                .PostAsync(ApiEndpoints.MyMusicianProfilesController.Post(), BuildStringContent(createDto));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.Created);
            MyMusicianProfileDto result = await DeserializeResponseMessageAsync<MyMusicianProfileDto>(responseMessage);

            result.Should().BeEquivalentTo(expectedDto, opt => opt.Excluding(r => r.Id).Excluding(r => r.DoublingInstruments));
            result.Id.Should().NotBeEmpty();
            result.DoublingInstruments.Count.Should().Be(1);
            result.DoublingInstruments.First().Should().BeEquivalentTo(expectedDto.DoublingInstruments.First(), opt => opt.Excluding(dto => dto.Id));
            result.DoublingInstruments.First().Id.Should().NotBeEmpty();
            responseMessage.Headers.Location.AbsolutePath.Should().Be($"/{ApiEndpoints.MusicianProfilesController.Get(result.Id)}");
        }

        [Test, Order(1001)]
        public async Task Should_Set_New_Project_Participation()
        {
            // Arrange
            MusicianProfile musicianProfile = MusicianProfileSeedData.PerformersHornMusicianProfile;
            Project project = ProjectSeedData.HoorayForHollywood;
            var dto = new SetMyProjectParticipationBodyDto
            {
                Comment = "Comment",
                StatusId = SelectValueMappingSeedData.ProjectParticipationStatusInnerMappings[0].Id
            };
            var expectedDto = new ProjectParticipationDto
            {
                CommentByPerformerInner = dto.Comment,
                ParticipationStatusInnerId = dto.StatusId,
                ParticipationStatusInner = "Interested",
                CreatedAt = FakeDateTime.UtcNow,
                CreatedBy = "Per Former",
                MusicianProfile = new ReducedMusicianProfileDto
                {
                    Id = musicianProfile.Id,
                    InstrumentName = "Horn",
                    Qualification = "Student"
                },
                Project = new ReducedProjectDto
                {
                    Id = project.Id,
                    Description = project.Description,
                    Code = project.Code,
                    ShortTitle = project.ShortTitle,
                    Title = project.Title
                }
            };

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_performer)
                .PutAsync(ApiEndpoints.MyMusicianProfilesController.SetProjectParticipation(
                    musicianProfile.Id,
                    project.Id), BuildStringContent(dto));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            ProjectParticipationDto result = await DeserializeResponseMessageAsync<ProjectParticipationDto>(responseMessage);
            result.Should().BeEquivalentTo(expectedDto, opt => opt.Excluding(dto => dto.Id));
            result.Id.Should().NotBeEmpty();
        }

        [Test, Order(1002)]
        public async Task Should_Set_Existing_Project_Participation()
        {
            // Arrange
            MusicianProfile musicianProfile = MusicianProfileSeedData.PerformerMusicianProfile;
            Project project = ProjectSeedData.Schneekönigin;

            var dto = new SetMyProjectParticipationBodyDto
            {
                Comment = "Comment",
                StatusId = SelectValueMappingSeedData.ProjectParticipationStatusInnerMappings[0].Id
            };
            ProjectParticipationDto expectedDto = ProjectParticipationDtoData.PerformerSchneeköniginParticipationForPerformer;
            expectedDto.CommentByPerformerInner = dto.Comment;
            expectedDto.ParticipationStatusInnerId = dto.StatusId;
            expectedDto.ParticipationStatusInner = "Interested";
            expectedDto.ModifiedAt = FakeDateTime.UtcNow;
            expectedDto.ModifiedBy = "Per Former";

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_performer)
                .PutAsync(ApiEndpoints.MyMusicianProfilesController.SetProjectParticipation(
                    musicianProfile.Id,
                    project.Id), BuildStringContent(dto));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            ProjectParticipationDto result = await DeserializeResponseMessageAsync<ProjectParticipationDto>(responseMessage);
            result.Should().BeEquivalentTo(expectedDto);
        }
    }
}
