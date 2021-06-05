using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Application.MusicianProfileApplication;
using Orso.Arpa.Application.ProjectApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.DtoTestData;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Api.Tests.IntegrationTests
{
    [TestFixture]
    public class MusicianProfilesControllerTests : IntegrationTestBase
    {
        [Test, Order(1)]
        public async Task Should_Get_ById()
        {
            // Arrange
            MusicianProfileDto expectedDto = MusicianProfileDtoData.PerformerProfile;

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .GetAsync(ApiEndpoints.MusicianProfilesController.Get(expectedDto.Id));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            MusicianProfileDto result = await DeserializeResponseMessageAsync<MusicianProfileDto>(responseMessage);
            result.Should().BeEquivalentTo(expectedDto);
        }

        private static IEnumerable<TestCaseData> s_projectParticipationData
        {
            get
            {
                yield return new TestCaseData(FakeUsers.Performer, MusicianProfileSeedData.PerformerMusicianProfile.Id, false, new List<ProjectParticipationDto> { ProjectParticipationDtoData.PerformerSchneeköniginParticipationForPerformer });
                yield return new TestCaseData(FakeUsers.Performer, MusicianProfileSeedData.PerformerMusicianProfile.Id, true, new List<ProjectParticipationDto> { ProjectParticipationDtoData.PerformerRockingXMasParticipationForPerformer, ProjectParticipationDtoData.PerformerSchneeköniginParticipationForPerformer });
                yield return new TestCaseData(FakeUsers.Staff, MusicianProfileSeedData.PerformerMusicianProfile.Id, false, new List<ProjectParticipationDto> { ProjectParticipationDtoData.PerformerSchneeköniginParticipationForStaff });
                yield return new TestCaseData(FakeUsers.Staff, MusicianProfileSeedData.PerformerMusicianProfile.Id, true, new List<ProjectParticipationDto> { ProjectParticipationDtoData.PerformerRockingXMasParticipationForStaff, ProjectParticipationDtoData.PerformerSchneeköniginParticipationForStaff });

            }
        }

        [Test, Order(2)]
        [TestCaseSource(nameof(s_projectParticipationData))]
        public async Task Should_Get_Project_Participations(User callingUser, Guid musicianProfileId, bool includeCompleted, IEnumerable<ProjectParticipationDto> expectedResult)
        {
            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(callingUser)
                .GetAsync(ApiEndpoints.MusicianProfilesController.GetProjectParticipations(musicianProfileId, includeCompleted));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            IEnumerable<ProjectParticipationDto> result = await DeserializeResponseMessageAsync<IEnumerable<ProjectParticipationDto>>(responseMessage);
            result.Should().BeEquivalentTo(expectedResult, opt => opt.WithStrictOrderingFor(dto => dto.Id));
        }

        [Test, Order(3)]
        public async Task Should_Not_Get_Project_Participations_Of_Different_Person()
        {
            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_performer)
                .GetAsync(ApiEndpoints.MusicianProfilesController.GetProjectParticipations(MusicianProfileSeedData.AdminMusicianProfile1.Id, true));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Test, Order(4)]
        public async Task Should_Get_With_Migrated_Section()
        {
            // Arrange

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .GetAsync(ApiEndpoints.MusicianProfilesController.Get(MusicianProfileSeedData.AdminMusicianProfile1.Id));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            MusicianProfileDto result = await DeserializeResponseMessageAsync<MusicianProfileDto>(responseMessage);
            result.InstrumentId.Should().Be(SectionSeedData.Alto.Id);
        }


        [Test, Order(1000)]
        public async Task Should_Set_Active_Status_Of_Musician_Profile()
        {
            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_performer)
                .PutAsync(ApiEndpoints.MusicianProfilesController.Put(MusicianProfileSeedData.PerformersDeactivatedTubaProfile.Id, true), null);

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        //[Test, Order(100)]
        //public async Task Should_Modify()
        //{
        //    // Arrange
        //    MusicianProfileDto musicianProfileToModify = MusicianProfileDtoData.PerformerProfile;
        //    var modifyDto = new MusicianProfileModifyBodyDto
        //    {
        //        IsMainProfile = false,
        //        IsDeactivated = false,

        //        LevelAssessmentPerformer = 1,
        //        LevelAssessmentStaff = 2,
        //        ProfilePreferencePerformer = 3,
        //        ProfilePreferenceStaff = 4,

        //        BackgroundPerformer = "revised: Background description",
        //        BackgroundStaff = "revised: Staff-Background description",
        //        SalaryComment = "revised: Salary only via PayPal, other payments not accepted!",

        //        QualificationId = SelectValueMappingSeedData.MusicianProfileQualificationMappings[3].Id,
        //        SalaryId = SelectValueMappingSeedData.MusicianProfileSalaryMappings[2].Id,
        //        InquiryStatusPerformerId = SelectValueMappingSeedData.MusicianProfileInquiryStatusPerformerMappings[0].Id,
        //        InquiryStatusStaffId = SelectValueMappingSeedData.MusicianProfileInquiryStatusStaffMappings[2].Id,

        //        // ToDo Collections
        //    };

        //    // Act
        //    HttpClient client = _authenticatedServer
        //        .CreateClient()
        //        .AuthenticateWith(_staff);

        //    HttpResponseMessage responseMessage = await client
        //        .PutAsync(ApiEndpoints.MusicianProfilesController.Put(musicianProfileToModify.Id), BuildStringContent(modifyDto));

        //    // Assert
        //    responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        //}

        //[Test, Order(10000)]
        //public async Task Should_Delete()
        //{
        //    // Arrange

        //    // Act
        //    HttpResponseMessage responseMessage = await _authenticatedServer
        //        .CreateClient()
        //        .AuthenticateWith(_staff)
        //        .DeleteAsync(ApiEndpoints.MusicianProfilesController.Delete(MusicianProfileDtoData.PerformerProfile.Id));

        //    // Assert
        //    responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        //}
    }
}
