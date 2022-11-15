using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Application.CurriculumVitaeReferenceApplication;
using Orso.Arpa.Application.EducationApplication;
using Orso.Arpa.Application.MusicianProfileApplication;
using Orso.Arpa.Application.MusicianProfileDeactivationApplication;
using Orso.Arpa.Application.ProjectApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Enums;
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
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            MusicianProfileDto result = await DeserializeResponseMessageAsync<MusicianProfileDto>(responseMessage);
            _ = result.Should().BeEquivalentTo(expectedDto);
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
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            IEnumerable<ProjectParticipationDto> result = await DeserializeResponseMessageAsync<IEnumerable<ProjectParticipationDto>>(responseMessage);
            _ = result.Should().BeEquivalentTo(expectedResult, opt => opt.WithStrictOrderingFor(dto => dto.Id));
        }

        [Test, Order(3)]
        public async Task Should_Not_Get_Project_Participations_Of_Different_Person()
        {
            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_performer)
                .GetAsync(ApiEndpoints.MusicianProfilesController.GetProjectParticipations(MusicianProfileSeedData.AdminMusicianSopranoProfile.Id, true));

            // Assert
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Test, Order(4)]
        public async Task Should_Get_Grouped()
        {
            // Arrange
            var expectedDtos = new List<GroupedMusicianProfileDto>
            {
                new GroupedMusicianProfileDto
                {
                    Person = ReducedPersonDtoData.Admin,
                    MusicianProfiles = new List<ReducedMusicianProfileDto>
                    {
                        ReducedMusicianProfileDtoData.AdminProfile1,
                        ReducedMusicianProfileDtoData.AdminProfile2
                    }
                },
                new GroupedMusicianProfileDto
                {
                    Person = ReducedPersonDtoData.Performer,
                    MusicianProfiles = new List<ReducedMusicianProfileDto>
                    {
                        ReducedMusicianProfileDtoData.PerformerProfile,
                        ReducedMusicianProfileDtoData.PerformerHornProfile,
                        ReducedMusicianProfileDtoData.PerformerDeactivatedTubaProfile,
                    }
                },
                new GroupedMusicianProfileDto
                {
                    Person = ReducedPersonDtoData.Staff,
                    MusicianProfiles = new List<ReducedMusicianProfileDto>
                    {
                        ReducedMusicianProfileDtoData.StaffProfile2,
                        ReducedMusicianProfileDtoData.StaffProfile1
                    }
                },
                new GroupedMusicianProfileDto
                {
                    Person = ReducedPersonDtoData.WithoutRole,
                    MusicianProfiles = new List<ReducedMusicianProfileDto>
                    {
                        ReducedMusicianProfileDtoData.WithoutRoleProfile
                    }
                }
            };

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .GetAsync(ApiEndpoints.MusicianProfilesController.Get());

            // Assert
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            IEnumerable<GroupedMusicianProfileDto> result = await DeserializeResponseMessageAsync<IEnumerable<GroupedMusicianProfileDto>>(responseMessage);
            _ = result.Should().BeEquivalentTo(expectedDtos, opt => opt.WithStrictOrderingFor(r => r.Person));
        }

        [Test, Order(100)]
        public async Task Should_Add_Education()
        {
            // Arrange
            var createDto = new EducationCreateBodyDto
            {
                TimeSpan = "1990-1996",
                Institution = "Hochschule für Musik und Darstellende Kunst Stuttgart",
                TypeId = SelectValueMappingSeedData.EducationTypeMappings[2].Id,
                Description = "Was für eine geniale Zeit an der HMDK!",
                SortOrder = 1,
            };
            var expectedDto = new EducationDto()
            {
                TimeSpan = createDto.TimeSpan,
                Institution = createDto.Institution,
                TypeId = createDto.TypeId,
                Description = createDto.Description,
                SortOrder = createDto.SortOrder,
                CreatedBy = _staff.DisplayName,
                CreatedAt = FakeDateTime.UtcNow
            };

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .PostAsync(ApiEndpoints.MusicianProfilesController.AddEducation(MusicianProfileSeedData.AdminMusicianSopranoProfile.Id), BuildStringContent(createDto));

            // Assert
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.Created);
            EducationDto result = await DeserializeResponseMessageAsync<EducationDto>(responseMessage);
            _ = result.Should().BeEquivalentTo(expectedDto, opt => opt.Excluding(r => r.Id));
            _ = result.Id.Should().NotBeEmpty();
            _ = responseMessage.Headers.Location.AbsolutePath.Should().Be($"/{ApiEndpoints.EducationsController.Get(result.Id)}");
        }

        [Test, Order(101)]
        public async Task Should_Add_CurriculumVitaeReference()
        {
            // Arrange
            var createDto = new CurriculumVitaeReferenceCreateBodyDto
            {
                TimeSpan = "1998-2000",
                Institution = "Kornwestheimer Symphoniker",
                TypeId = SelectValueMappingSeedData.CurriculumVitaeReferenceTypeMappings[1].Id,
                Description = "Mozart, Strauss Solokonzerte",
                SortOrder = 1,
            };
            var expectedDto = new CurriculumVitaeReferenceDto()
            {
                TimeSpan = createDto.TimeSpan,
                Institution = createDto.Institution,
                TypeId = createDto.TypeId,
                Description = createDto.Description,
                SortOrder = createDto.SortOrder,
                CreatedBy = _staff.DisplayName,
                CreatedAt = FakeDateTime.UtcNow
            };

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .PostAsync(ApiEndpoints.MusicianProfilesController.AddCurriculumVitaeReference(MusicianProfileSeedData.PerformerMusicianProfile.Id), BuildStringContent(createDto));

            // Assert
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.Created);
            CurriculumVitaeReferenceDto result = await DeserializeResponseMessageAsync<CurriculumVitaeReferenceDto>(responseMessage);
            _ = result.Should().BeEquivalentTo(expectedDto, opt => opt.Excluding(r => r.Id));
            _ = result.Id.Should().NotBeEmpty();
            _ = responseMessage.Headers.Location.AbsolutePath.Should().Be($"/{ApiEndpoints.CurriculumVitaeReferencesController.Get(result.Id)}");
        }

        [Test, Order(102)]
        public async Task Should_Deactivate_Musician_Profile()
        {
            // Arrange
            var createDto = new MusicianProfileDeactivationCreateBodyDto
            {
                DeactivationStart = FakeDateTime.UtcNow.AddDays(20),
                Purpose = "Ich werde am Handgelenk operiert und werde einige Wochen nicht Klavier spielen können."
            };
            var expectedDto = new MusicianProfileDeactivationDto()
            {
                DeactivationStart = createDto.DeactivationStart,
                Purpose = createDto.Purpose,
                CreatedBy = _performer.DisplayName,
                CreatedAt = FakeDateTime.UtcNow
            };

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_performer)
                .PostAsync(ApiEndpoints.MusicianProfilesController.AddDeactivation(MusicianProfileSeedData.PerformerMusicianProfile.Id), BuildStringContent(createDto));

            // Assert
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.Created);
            MusicianProfileDeactivationDto result = await DeserializeResponseMessageAsync<MusicianProfileDeactivationDto>(responseMessage);
            _ = result.Should().BeEquivalentTo(expectedDto, opt => opt.Excluding(r => r.Id));
            _ = result.Id.Should().NotBeEmpty();
        }

        [Test, Order(103)]
        public async Task Should_Reactivate_Musician_Profile()
        {
            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_performer)
                .DeleteAsync(ApiEndpoints.MusicianProfilesController.RemoveDeactivation(MusicianProfileSeedData.PerformersDeactivatedTubaProfile.Id));

            // Assert
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Test, Order(1000)]
        public async Task Should_Modify()
        {
            // Arrange
            MusicianProfileDto musicianProfileToModify = MusicianProfileDtoData.PerformersHornMusicianProfile;
            var modifyDto = new MusicianProfileModifyBodyDto
            {
                IsMainProfile = true,

                LevelAssessmentInner = 1,
                LevelAssessmentTeam = 2,
                ProfilePreferenceInner = 3,
                ProfilePreferenceTeam = 4,

                BackgroundInner = "revised: Background description",
                BackgroundTeam = "revised: Staff-Background description",
                SalaryComment = "revised: Salary only via PayPal, other payments not accepted!",

                QualificationId = SelectValueMappingSeedData.MusicianProfileQualificationMappings[3].Id,
                SalaryId = SelectValueMappingSeedData.MusicianProfileSalaryMappings[2].Id,
                InquiryStatusInner = MusicianProfileInquiryStatus.ForContactsOnly,
                InquiryStatusTeam = MusicianProfileInquiryStatus.Gladly,
            };
            modifyDto.PreferredPositionsTeamIds.Add(SelectValueSectionSeedData.HornSolo.Id);
            modifyDto.PreferredPartsTeam.Add(2);
            modifyDto.PreferredPartsInner.Add(3);
            var expectedDto = new MusicianProfileDto
            {
                DoublingInstruments = musicianProfileToModify.DoublingInstruments,
                BackgroundInner = modifyDto.BackgroundInner,
                BackgroundTeam = modifyDto.BackgroundTeam,
                CreatedAt = musicianProfileToModify.CreatedAt,
                CreatedBy = musicianProfileToModify.CreatedBy,
                SalaryComment = modifyDto.SalaryComment,
                SalaryId = modifyDto.SalaryId,
                InquiryStatusInner = (Domain.Enums.MusicianProfileInquiryStatus)modifyDto.InquiryStatusInner,
                Id = musicianProfileToModify.Id,
                InquiryStatusTeam = (Domain.Enums.MusicianProfileInquiryStatus)modifyDto.InquiryStatusTeam,
                InstrumentId = musicianProfileToModify.InstrumentId,
                IsMainProfile = true,
                LevelAssessmentInner = modifyDto.LevelAssessmentInner,
                LevelAssessmentTeam = modifyDto.LevelAssessmentTeam,
                ModifiedAt = FakeDateTime.UtcNow,
                ModifiedBy = "Staff Member",
                PersonId = musicianProfileToModify.PersonId,
                PreferredPartsInner = modifyDto.PreferredPartsInner,
                PreferredPartsTeam = modifyDto.PreferredPartsTeam,
                PreferredPositionsTeamIds = modifyDto.PreferredPositionsTeamIds,
                ProfilePreferenceInner = modifyDto.ProfilePreferenceInner,
                ProfilePreferenceTeam = modifyDto.ProfilePreferenceTeam,
                QualificationId = modifyDto.QualificationId,
                Educations = musicianProfileToModify.Educations,
                CurriculumVitaeReferences = musicianProfileToModify.CurriculumVitaeReferences
            };

            HttpClient client = _authenticatedServer
                            .CreateClient()
                            .AuthenticateWith(_staff);

            // Act
            HttpResponseMessage responseMessage = await client
                .PutAsync(ApiEndpoints.MusicianProfilesController.Put(musicianProfileToModify.Id), BuildStringContent(modifyDto));

            // Assert
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            MusicianProfileDto result = await DeserializeResponseMessageAsync<MusicianProfileDto>(responseMessage);
            _ = result.Should().BeEquivalentTo(expectedDto);

            // check if former main profile is not main profile anymore
            HttpResponseMessage getResponseMessage = await client
                .GetAsync(ApiEndpoints.MusicianProfilesController.Get(MusicianProfileSeedData.PerformerMusicianProfile.Id));

            _ = getResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            MusicianProfileDto getResult = await DeserializeResponseMessageAsync<MusicianProfileDto>(getResponseMessage);
            _ = getResult.IsMainProfile.Should().BeFalse();
        }

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
