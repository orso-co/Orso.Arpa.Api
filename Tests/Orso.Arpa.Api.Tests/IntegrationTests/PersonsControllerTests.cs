using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Application.DoublingInstrumentApplication;
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
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            IEnumerable<PersonDto> result = await DeserializeResponseMessageAsync<IEnumerable<PersonDto>>(responseMessage);
            _ = result.Should().BeEquivalentTo(PersonDtoData.Persons, opt => opt.WithStrictOrdering());
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
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            PersonDto result = await DeserializeResponseMessageAsync<PersonDto>(responseMessage);
            _ = result.Should().BeEquivalentTo(expectedDto);
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
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            IEnumerable<MusicianProfileDto> result = await DeserializeResponseMessageAsync<IEnumerable<MusicianProfileDto>>(responseMessage);
            _ = result.Should().BeEquivalentTo(expectedDtos, opt => opt.WithStrictOrdering());
        }

        [Test, Order(3)]
        public async Task Should_Invite_Persons()
        {
            var inviteDto = new PersonInviteDto();
            inviteDto.PersonIds.Add(PersonTestSeedData.PersonWithoutUser.Id);
            inviteDto.PersonIds.Add(PersonTestSeedData.Performer.Id);
            inviteDto.PersonIds.Add(PersonTestSeedData.PersonWithoutEmail.Id);
            inviteDto.PersonIds.Add(PersonTestSeedData.PersonWithMultipleEmails.Id);

            var expectedDto = new PersonInviteResultDto();
            expectedDto.PersonsAlreadyRegistered.Add("Per Former");
            expectedDto.PersonsWithMultipleEmailAddresses.Add("Person Multiple", new List<string> { "person@withmultiple2.email", "person@withmultiple.email" });
            expectedDto.PersonsWithoutEmailAddress.Add("Person Withoutemail");
            expectedDto.SuccessfulInvites.Add("Person Without", "person@without.user");
            expectedDto.SuccessfulInvites.Add("Person Multiple", "person@withmultiple2.email");

            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .PostAsync(ApiEndpoints.PersonsController.Invite(), BuildStringContent(inviteDto));

            // Assert
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            PersonInviteResultDto result = await DeserializeResponseMessageAsync<PersonInviteResultDto>(responseMessage);
            _ = result.Should().BeEquivalentTo(expectedDto);
            _ = _fakeSmtpServer.ReceivedEmailCount.Should().Be(2);
            _ = _fakeSmtpServer.ReceivedEmail.Select(em => em.ToAddresses[0].Address).Should().BeEquivalentTo(expectedDto.SuccessfulInvites.Values);
        }

        [Test, Order(100)]
        public async Task Should_Modify()
        {
            // Arrange
            Person personToModify = PersonTestSeedData.LockedOutUser;
            var modifyDto = new PersonModifyBodyDto
            {
                GivenName = "Tommy",
                Surname = "Bones",
                AboutMe = "This is about me - bone",
                GenderId = SelectValueMappingSeedData.PersonGenderMappings[0].Id,
                DateOfBirth = new DateTime(1960, 6, 6),
                BirthName = "Bonny",
                Birthplace = "Honolulu",
                PersonBackgroundTeam = "This is a wonderful person our staff met in Berlin",
                ContactViaId = PersonTestSeedData.Performer.Id,
                ExperienceLevel = 3,
                Reliability = 2,
                GeneralPreference = 4
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
                DateOfBirth = modifyDto.DateOfBirth,
                BirthName = modifyDto.BirthName,
                Birthplace = modifyDto.Birthplace,
                PersonBackgroundTeam = modifyDto.PersonBackgroundTeam,
                ContactVia = ReducedPersonDtoData.Performer,
                Gender = SelectValueDtoData.Female,
                ExperienceLevel = modifyDto.ExperienceLevel,
                Reliability = modifyDto.Reliability,
                GeneralPreference = modifyDto.GeneralPreference
            };
            expectedDto.ContactsRecommended.Add(ReducedPersonDtoData.UnconfirmedUser);

            // Act
            HttpClient client = _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff);

            HttpResponseMessage responseMessage = await client
                .PutAsync(ApiEndpoints.PersonsController.Put(personToModify.Id), BuildStringContent(modifyDto));

            // Assert
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);

            HttpResponseMessage getMessage = await client
                .GetAsync(ApiEndpoints.PersonsController.Get(personToModify.Id));

            _ = getMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            PersonDto result = await DeserializeResponseMessageAsync<PersonDto>(getMessage);

            _ = result.Should().BeEquivalentTo(expectedDto);
        }

        [Test, Order(101)]
        public async Task Should_Add_StakeholderGroup()
        {
            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .PostAsync(ApiEndpoints.PersonsController.StakeholderGroups(
                    PersonTestSeedData.Person1WithSameEmail.Id,
                    SectionSeedData.Performers.Id), null);

            // Assert
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Test, Order(102)]
        public async Task Should_Remove_StakeholderGroup()
        {
            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .DeleteAsync(ApiEndpoints.PersonsController.StakeholderGroups(
                    PersonTestSeedData.Performer.Id,
                    SectionSeedData.Choir.Id));

            // Assert
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Test, Order(1000)]
        public async Task Should_Create()
        {
            // Arrange
            var createDto = new PersonCreateDto
            {
                GivenName = "Per",
                Surname = "Son",
                AboutMe = "This is about me",
                GenderId = SelectValueMappingSeedData.PersonGenderMappings[0].Id,
                BirthName = "Daughter",
                DateOfBirth = new DateTime(1990, 1, 1),
                Birthplace = "Buxdehude",
                PersonBackgroundTeam = "This is a crazy person, but good musician",
                ContactViaId = PersonTestSeedData.DeletedUser.Id,
                ExperienceLevel = 2,
                Reliability = 3,
                GeneralPreference = 4
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
                Gender = SelectValueDtoData.Female,
                BirthName = createDto.BirthName,
                DateOfBirth = createDto.DateOfBirth.Value,
                Birthplace = createDto.Birthplace,
                PersonBackgroundTeam = createDto.PersonBackgroundTeam,
                ContactVia = ReducedPersonDtoData.DeletedUser,
                ExperienceLevel = createDto.ExperienceLevel,
                Reliability = createDto.Reliability,
                GeneralPreference = createDto.GeneralPreference
            };

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .PostAsync(ApiEndpoints.PersonsController.Post(), BuildStringContent(createDto));

            // Assert
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.Created);
            PersonDto result = await DeserializeResponseMessageAsync<PersonDto>(responseMessage);

            _ = result.Should().BeEquivalentTo(expectedDto, opt => opt
                .Excluding(dto => dto.Id));
            _ = result.Id.Should().NotBeEmpty();
            _ = responseMessage.Headers.Location.AbsolutePath.Should().Be($"/{ApiEndpoints.PersonsController.Get(result.Id)}");
        }

        [Test, Order(1002)]
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
                PersonId = PersonDtoData.UnconfirmedUser.Id,
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
            _fakeSmtpServer.ClearReceivedEmail();

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .PostAsync(ApiEndpoints.PersonsController.AddMusicianProfile(PersonTestSeedData.UnconfirmedUser.Id), BuildStringContent(createDto));

            // Assert
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.Created);
            MusicianProfileDto result = await DeserializeResponseMessageAsync<MusicianProfileDto>(responseMessage);

            _ = result.Should().BeEquivalentTo(expectedDto, opt => opt.Excluding(r => r.Id).Excluding(r => r.DoublingInstruments));
            _ = result.Id.Should().NotBeEmpty();
            _ = result.DoublingInstruments.Count.Should().Be(1);
            _ = result.DoublingInstruments[0].Should().BeEquivalentTo(expectedDto.DoublingInstruments[0], opt => opt.Excluding(dto => dto.Id));
            _ = result.DoublingInstruments[0].Id.Should().NotBeEmpty();
            _ = responseMessage.Headers.Location.AbsolutePath.Should().Be($"/{ApiEndpoints.MusicianProfilesController.Get(result.Id)}");
            EvaluateSimpleEmail("New MuPro for Unconfirmed User: Clarinet", "kbb@orso.co");
        }

        [Test, Order(1003)]
        public async Task Should_Add_Minimal_MusicianProfile()
        {
            // Arrange
            var createDto = new MusicianProfileCreateBodyDto
            {
                InstrumentId = SectionSeedData.AcousticGuitar.Id,
                QualificationId = SelectValueMappingSeedData.MusicianProfileQualificationMappings[2].Id,
            };

            var expectedDto = new MusicianProfileDto
            {
                InstrumentId = createDto.InstrumentId,
                QualificationId = createDto.QualificationId,
                PersonId = PersonDtoData.PersonWithMultipleEmails.Id,
                CreatedBy = _staff.DisplayName,
                CreatedAt = FakeDateTime.UtcNow,
                IsMainProfile = true
            };
            _fakeSmtpServer.ClearReceivedEmail();

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .PostAsync(ApiEndpoints.PersonsController.AddMusicianProfile(PersonTestSeedData.PersonWithMultipleEmails.Id), BuildStringContent(createDto));

            // Assert
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.Created);
            MusicianProfileDto result = await DeserializeResponseMessageAsync<MusicianProfileDto>(responseMessage);

            _ = result.Should().BeEquivalentTo(expectedDto, opt => opt.Excluding(dto => dto.Id));
            _ = result.Id.Should().NotBeEmpty();
            _ = responseMessage.Headers.Location.AbsolutePath.Should().Be($"/{ApiEndpoints.MusicianProfilesController.Get(result.Id)}");
            EvaluateSimpleEmail("New MuPro for Person Multiple: Acoustic Guitar (Orchestra)", "kbb@orso.co");
        }

        [Test, Order(1001)]
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
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
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
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
