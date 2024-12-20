using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Application.MeApplication.Model;
using Orso.Arpa.Domain.MusicianProfileDomain.Enums;
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
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            IList<MyMusicianProfileDto> result = await DeserializeResponseMessageAsync<IList<MyMusicianProfileDto>>(responseMessage);
            _ = result.Should().BeEquivalentTo(expectedDtos, opt => opt.WithStrictOrdering());
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
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            MyMusicianProfileDto result = await DeserializeResponseMessageAsync<MyMusicianProfileDto>(responseMessage);
            _ = result.Should().BeEquivalentTo(expectedDto);
        }

        [Test, Order(999)]
        public async Task Should_Return_Unprocessable_Entity_If_Collection_Is_Null()
        {
            // Arrange
            var createDto = new MyMusicianProfileCreateDto
            {
                InstrumentId = Guid.Parse("c2cfb7a0-4981-4dda-b988-8ba74957f6a4"),
                LevelAssessmentInner = 1,
                InquiryStatusInner = MusicianProfileInquiryStatus.ForContactsOnly,
                PreferredPositionsInnerIds = null,
            };
            var expectedResult = new ValidationProblemDetails
            {
                Status = 422,
                Type = "https://tools.ietf.org/html/rfc4918#section-11.2"
            };
            expectedResult.Errors.Add("PreferredPositionsInnerIds", ["'Preferred Positions Inner Ids' must not be empty."]);

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_performer)
                .PostAsync(ApiEndpoints.MyMusicianProfilesController.Post(), BuildStringContent(createDto));

            // Assert
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            ValidationProblemDetails result = await DeserializeResponseMessageAsync<ValidationProblemDetails>(responseMessage);
            _ = result.Should().BeEquivalentTo(expectedResult, opt => opt.Excluding(r => r.Extensions));
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

            var expectedDto = new MyMusicianProfileDto
            {
                Instrument = SectionDtoData.Clarinet,
                LevelAssessmentInner = createDto.LevelAssessmentInner,
                CreatedBy = _performer.DisplayName,
                CreatedAt = FakeDateTime.UtcNow,
                PersonId = _performer.PersonId
            };
            expectedDto.PreferredPositionsInnerIds.Add(SelectValueSectionSeedData.ClarinetCoach.Id);
            expectedDto.PreferredPartsInner.Add(2);
            expectedDto.PreferredPartsInner.Add(4);

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_performer)
                .PostAsync(ApiEndpoints.MyMusicianProfilesController.Post(), BuildStringContent(createDto));

            // Assert
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.Created);
            MyMusicianProfileDto result = await DeserializeResponseMessageAsync<MyMusicianProfileDto>(responseMessage);

            _ = result.Should().BeEquivalentTo(expectedDto, opt => opt.Excluding(r => r.Id).Excluding(r => r.DoublingInstruments));
            _ = result.Id.Should().NotBeEmpty();
            _ = responseMessage.Headers.Location.AbsolutePath.Should().Be($"/{ApiEndpoints.MusicianProfilesController.Get(result.Id)}");
        }

        [Test, Order(1003)]
        public async Task Should_Modify_My_Musician_Profile()
        {
            // Arrange
            MyMusicianProfileDto musicianProfileToModify = MyMusicianProfileDtoData.PerformersHornMusicianProfile;
            var modifyDto = new MyMusicianProfileModifyBodyDto
            {
                IsMainProfile = true,

                LevelAssessmentInner = 1,
                ProfilePreferenceInner = 3,

                BackgroundInner = "revised: Background description",

                InquiryStatusInner = MusicianProfileInquiryStatus.ForContactsOnly,
            };
            modifyDto.PreferredPositionsInnerIds.Add(SelectValueSectionSeedData.HornLow.Id);
            modifyDto.PreferredPartsInner.Add(3);

            var expectedDto = new MyMusicianProfileDto
            {
                DoublingInstruments = musicianProfileToModify.DoublingInstruments,
                BackgroundInner = modifyDto.BackgroundInner,
                CreatedAt = musicianProfileToModify.CreatedAt,
                CreatedBy = musicianProfileToModify.CreatedBy,
                InquiryStatusInner = (MusicianProfileInquiryStatus)modifyDto.InquiryStatusInner,
                Id = musicianProfileToModify.Id,
                Instrument = musicianProfileToModify.Instrument,
                IsMainProfile = true,
                LevelAssessmentInner = modifyDto.LevelAssessmentInner,
                ModifiedAt = FakeDateTime.UtcNow,
                ModifiedBy = "Per Former",
                PersonId = musicianProfileToModify.PersonId,
                PreferredPartsInner = modifyDto.PreferredPartsInner,
                PreferredPositionsInnerIds = modifyDto.PreferredPositionsInnerIds,
                ProfilePreferenceInner = modifyDto.ProfilePreferenceInner,
                Educations = musicianProfileToModify.Educations,
                CurriculumVitaeReferences = musicianProfileToModify.CurriculumVitaeReferences
            };

            HttpClient client = _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_performer);

            // Act
            HttpResponseMessage responseMessage = await client
                .PutAsync(ApiEndpoints.MyMusicianProfilesController.Put(musicianProfileToModify.Id), BuildStringContent(modifyDto));

            // Assert
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            MyMusicianProfileDto result = await DeserializeResponseMessageAsync<MyMusicianProfileDto>(responseMessage);
            _ = result.Should().BeEquivalentTo(expectedDto);

            // check if former main profile is not main profile anymore
            HttpResponseMessage getResponseMessage = await client
                .GetAsync(ApiEndpoints.MyMusicianProfilesController.GetById(MusicianProfileSeedData.PerformerMusicianProfile.Id));

            _ = getResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            MyMusicianProfileDto getResult = await DeserializeResponseMessageAsync<MyMusicianProfileDto>(getResponseMessage);
            _ = getResult.IsMainProfile.Should().BeFalse();
        }
    }
}
