using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Application.MusicianProfileApplication;
using Orso.Arpa.Application.PersonApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.DtoTestData;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Api.Tests.IntegrationTests
{
    [TestFixture]
    public class PersonsControllerTests : IntegrationTestBase
    {
        [Test, Order(1)]
        public async Task Should_Get_All()
        {
            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .GetAsync(ApiEndpoints.PersonsController.Get());

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            IEnumerable<PersonDto> result = await DeserializeResponseMessageAsync<IEnumerable<PersonDto>>(responseMessage);
            result.Should().BeEquivalentTo(PersonDtoData.Persons);
        }

        [Test, Order(2)]
        public async Task Should_Get_ById()
        {
            // Arrange
            PersonDto expectedDto = PersonDtoData.Performer;

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .GetAsync(ApiEndpoints.PersonsController.Get(expectedDto.Id));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            PersonDto result = await DeserializeResponseMessageAsync<PersonDto>(responseMessage);
            result.Should().BeEquivalentTo(expectedDto);
        }

        private static IEnumerable<TestCaseData> s_musicianProfileData
        {
            get
            {
                yield return new TestCaseData(false, new List<MusicianProfileDto> {
                    MusicianProfileDtoData.PerformerProfile,
                    MusicianProfileDtoData.PerformersHornMusicianProfile,
                    });
                yield return new TestCaseData(true, new List<MusicianProfileDto> {
                    MusicianProfileDtoData.PerformerProfile,
                    MusicianProfileDtoData.PerformersDeactivatedTubaProfile,
                    MusicianProfileDtoData.PerformersHornMusicianProfile
                    });
            }
        }

        [Test, Order(2)]
        [TestCaseSource(nameof(s_musicianProfileData))]
        public async Task Should_Get_All_Profiles_Of_A_Person(bool includeDeactivated, IList<MusicianProfileDto> expectedDtos)
        {
            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .GetAsync(ApiEndpoints.PersonsController.GetMusicianProfiles(_performer.PersonId, includeDeactivated));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            IEnumerable<MusicianProfileDto> result = await DeserializeResponseMessageAsync<IEnumerable<MusicianProfileDto>>(responseMessage);
            result.Should().BeEquivalentTo(expectedDtos, opt => opt.WithStrictOrdering());
        }

        [Test, Order(100)]
        public async Task Should_Modify()
        {
            // Arrange
            Person personToModify = PersonTestSeedData.LockedOutUser;
            var modifyDto = new PersonModifyBodyDto
            {
                GivenName = "Tom2",
                Surname = "Bone2",
                AboutMe = "This is about me - bone"
            };

            var expectedDto = new PersonDto
            {
                Id = personToModify.Id,
                GivenName = modifyDto.GivenName,
                Surname = modifyDto.Surname,
                AboutMe = modifyDto.AboutMe,
                CreatedBy = "anonymous",
                CreatedAt = FakeDateTime.UtcNow,
                ModifiedAt = FakeDateTime.UtcNow,
                ModifiedBy = _staff.DisplayName,
            };

            // Act
            HttpClient client = _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff);

            HttpResponseMessage responseMessage = await client
                .PutAsync(ApiEndpoints.PersonsController.Put(personToModify.Id), BuildStringContent(modifyDto));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);

            HttpResponseMessage getMessage = await client
                .GetAsync(ApiEndpoints.PersonsController.Get(personToModify.Id));

            getMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            PersonDto result = await DeserializeResponseMessageAsync<PersonDto>(getMessage);

            result.Should().BeEquivalentTo(expectedDto);
        }

        [Test, Order(1000)]
        public async Task Should_Create()
        {
            // Arrange
            var createDto = new PersonCreateDto
            {
                GivenName = "Per",
                Surname = "Son",
                AboutMe = "This is about me"
            };

            var expectedDto = new PersonDto
            {
                GivenName = createDto.GivenName,
                Surname = createDto.Surname,
                AboutMe = createDto.AboutMe,
                CreatedBy = _staff.DisplayName,
                CreatedAt = FakeDateTime.UtcNow,
                ModifiedAt = null,
                ModifiedBy = null,
            };

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .PostAsync(ApiEndpoints.PersonsController.Post(), BuildStringContent(createDto));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.Created);
            PersonDto result = await DeserializeResponseMessageAsync<PersonDto>(responseMessage);

            result.Should().BeEquivalentTo(expectedDto, opt => opt
                .Excluding(dto => dto.Id));
            result.Id.Should().NotBeEmpty();
            responseMessage.Headers.Location.AbsolutePath.Should().Be($"/{ApiEndpoints.PersonsController.Get(result.Id)}");
        }

        [Test, Order(1001)]
        public async Task Should_Add_MusicianProfile()
        {
            // Arrange
            var createDto = new MusicianProfileCreateBodyDto
            {
                InstrumentId = SectionSeedData.Clarinet.Id,
                QualificationId = SelectValueMappingSeedData.MusicianProfileQualificationMappings[2].Id,
            };
            createDto.PreferredPositionsInnerIds.Add(SelectValueSectionSeedData.ClarinetCoach.Id);
            createDto.PreferredPositionsTeamIds.Add(SelectValueSectionSeedData.ClarinetSolo.Id);
            createDto.PreferredPartsInner.Add(2);
            createDto.PreferredPartsInner.Add(4);
            createDto.PreferredPartsTeam.Add(1);

            var createDoublingInstrumentDto = new DoublingInstrumentCreateBodyDto
            {
                InstrumentId = SectionSeedData.EbClarinet.Id,
                AvailabilityId = SelectValueMappingSeedData.MusicianProfileSectionInstrumentAvailabilityMappings[0].Id,
                LevelAssessmentTeam = 3,
                LevelAssessmentInner = 4,
                Comment = "my comment"
            };
            createDto.DoublingInstruments.Add(createDoublingInstrumentDto);

            var expectedDto = new MusicianProfileDto
            {
                InstrumentId = createDto.InstrumentId,
                QualificationId = createDto.QualificationId,
                PersonId = PersonDtoData.LockedOutUser.Id,
                CreatedBy = _staff.DisplayName,
                CreatedAt = FakeDateTime.UtcNow,
                IsMainProfile = true
            };
            expectedDto.PreferredPositionsInnerIds.Add(SelectValueSectionSeedData.ClarinetCoach.Id);
            expectedDto.PreferredPositionsTeamIds.Add(SelectValueSectionSeedData.ClarinetSolo.Id);
            expectedDto.PreferredPartsInner.Add(2);
            expectedDto.PreferredPartsInner.Add(4);
            expectedDto.PreferredPartsTeam.Add(1);
            expectedDto.DoublingInstruments.Add(new DoublingInstrumentDto
            {
                AvailabilityId = createDoublingInstrumentDto.AvailabilityId,
                Comment = createDoublingInstrumentDto.Comment,
                InstrumentId = createDoublingInstrumentDto.InstrumentId,
                LevelAssessmentTeam = createDoublingInstrumentDto.LevelAssessmentTeam,
                CreatedAt = FakeDateTime.UtcNow,
                CreatedBy = _staff.DisplayName,
                LevelAssessmentInner = createDoublingInstrumentDto.LevelAssessmentInner
            });

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .PostAsync(ApiEndpoints.PersonsController.AddMusicianProfile(PersonDtoData.LockedOutUser.Id), BuildStringContent(createDto));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.Created);
            MusicianProfileDto result = await DeserializeResponseMessageAsync<MusicianProfileDto>(responseMessage);

            result.Should().BeEquivalentTo(expectedDto, opt => opt.Excluding(r => r.Id).Excluding(r => r.DoublingInstruments));
            result.Id.Should().NotBeEmpty();
            result.DoublingInstruments.Count.Should().Be(1);
            result.DoublingInstruments.First().Should().BeEquivalentTo(expectedDto.DoublingInstruments.First(), opt => opt.Excluding(dto => dto.Id));
            result.DoublingInstruments.First().Id.Should().NotBeEmpty();
            responseMessage.Headers.Location.AbsolutePath.Should().Be($"/{ApiEndpoints.MusicianProfilesController.Get(result.Id)}");
        }

        [Test, Order(1101)]
        public async Task Should_Not_Add_MusicianProfile_Due_To_Missing_Mandatory_Field()
        {
            // Arrange
            var createDto = new MusicianProfileCreateBodyDto
            {
                InstrumentId = SectionSeedData.Euphonium.Id,
                // -> this is the missing mandatory field: QualificationId 
            };

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .PostAsync(ApiEndpoints.PersonsController.AddMusicianProfile(FakePersons.Performer.Id), BuildStringContent(createDto));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        }

        [Test, Order(10000)]
        public async Task Should_Delete()
        {
            // Arrange

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .DeleteAsync(ApiEndpoints.PersonsController.Delete(PersonTestSeedData.UnconfirmedUser.Id));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
