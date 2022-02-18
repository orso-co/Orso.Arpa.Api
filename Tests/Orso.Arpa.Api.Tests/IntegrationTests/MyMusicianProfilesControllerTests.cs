using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Application.MusicianProfileApplication;
using Orso.Arpa.Application.MyMusicianProfileApplication;
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
            MyMusicianProfileDto result = await DeserializeResponseMessageAsync<MyMusicianProfileDto>(responseMessage);
            result.Should().BeEquivalentTo(expectedDto);
        }

        [Test, Order(999)]
        public async Task Should_Return_Unprocessable_Entity_If_Collection_Is_Null()
        {
            // Arrange
            var createDto = new MyMusicianProfileCreateDto
            {
                InstrumentId = Guid.Parse("c2cfb7a0-4981-4dda-b988-8ba74957f6a4"),
                LevelAssessmentInner = 1,
                InquiryStatusInnerId = Guid.Parse("90b5cfa9-890b-4b89-a750-646f3a26db23"),
                PreferredPositionsInnerIds = null,
                DoublingInstruments = null
            };
            var expectedResult = new ValidationProblemDetails
            {
                Status = 422,
                Type = "https://tools.ietf.org/html/rfc4918#section-11.2"
            };
            expectedResult.Errors.Add("DoublingInstruments", new string[] { "'Doubling Instruments' must not be empty." });
            expectedResult.Errors.Add("PreferredPositionsInnerIds", new string[] { "'Preferred Positions Inner Ids' must not be empty." });

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_performer)
                .PostAsync(ApiEndpoints.MyMusicianProfilesController.Post(), BuildStringContent(createDto));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            ValidationProblemDetails result = await DeserializeResponseMessageAsync<ValidationProblemDetails>(responseMessage);
            result.Should().BeEquivalentTo(expectedResult, opt => opt.Excluding(r => r.Extensions));
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

            var createDoublingInstrumentDto = new MyDoublingInstrumentCreateBodyDto
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

                InquiryStatusInnerId = SelectValueMappingSeedData.MusicianProfileInquiryStatusInnerMappings[0].Id,
            };
            modifyDto.PreferredPositionsInnerIds.Add(SelectValueSectionSeedData.HornLow.Id);
            modifyDto.PreferredPartsInner.Add(3);

            var expectedDto = new MyMusicianProfileDto
            {
                DoublingInstruments = musicianProfileToModify.DoublingInstruments,
                BackgroundInner = modifyDto.BackgroundInner,
                CreatedAt = musicianProfileToModify.CreatedAt,
                CreatedBy = musicianProfileToModify.CreatedBy,
                InquiryStatusInnerId = modifyDto.InquiryStatusInnerId,
                Id = musicianProfileToModify.Id,
                InstrumentId = musicianProfileToModify.InstrumentId,
                IsMainProfile = true,
                LevelAssessmentInner = modifyDto.LevelAssessmentInner,
                ModifiedAt = FakeDateTime.UtcNow,
                ModifiedBy = "Per Former",
                PersonId = musicianProfileToModify.PersonId,
                PreferredPartsInner = modifyDto.PreferredPartsInner,
                PreferredPositionsInnerIds = modifyDto.PreferredPositionsInnerIds,
                ProfilePreferenceInner = modifyDto.ProfilePreferenceInner,
            };
            expectedDto.Educations = musicianProfileToModify.Educations;
            expectedDto.CurriculumVitaeReferences = musicianProfileToModify.CurriculumVitaeReferences;

            HttpClient client = _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_performer);

            // Act
            HttpResponseMessage responseMessage = await client
                .PutAsync(ApiEndpoints.MyMusicianProfilesController.Put(musicianProfileToModify.Id), BuildStringContent(modifyDto));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            MusicianProfileDto result = await DeserializeResponseMessageAsync<MusicianProfileDto>(responseMessage);
            result.Should().BeEquivalentTo(expectedDto);

            // check if former main profile is not main profile anymore
            HttpResponseMessage getResponseMessage = await client
                .GetAsync(ApiEndpoints.MyMusicianProfilesController.GetById(MusicianProfileSeedData.PerformerMusicianProfile.Id));

            getResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            MyMusicianProfileDto getResult = await DeserializeResponseMessageAsync<MyMusicianProfileDto>(getResponseMessage);
            getResult.IsMainProfile.Should().BeFalse();
        }
    }
}
