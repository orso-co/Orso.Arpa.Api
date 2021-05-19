using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Application.MusicianProfileApplication;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.DtoTestData;
using Orso.Arpa.Tests.Shared.FakeData;

namespace Orso.Arpa.Api.Tests.IntegrationTests
{
    [TestFixture]
    public class MusicianProfilesControllerTests : IntegrationTestBase
    {
        [Test, Order(1)]
        public async Task Should_Get_ById()
        {
            // Arrange
            MusicianProfileDto expectedDto = MusicianProfileDtoData.Trombonist;

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_performer)
                .GetAsync(ApiEndpoints.MusicianProfilesController.Get(expectedDto.Id));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            MusicianProfileDto result = await DeserializeResponseMessageAsync<MusicianProfileDto>(responseMessage);
            result.Should().BeEquivalentTo(expectedDto);
        }

        //[Test, Order(2)]
        //public async Task Should_Get_All_Profiles_Of_A_Person()
        //{
        //    // Arrange
        //    IList<MusicianProfileDto> expectedDto = MusicianProfileDtoData.ProfilesForTromboneAndEuphoniumPlayer;

        //    // Act
        //    HttpResponseMessage responseMessage = await _authenticatedServer
        //        .CreateClient()
        //        .AuthenticateWith(_performer)
        //        .GetAsync(ApiEndpoints.MusicianProfilesController.GetAllProfilesOfAPerson(expectedDto.Id));

        //    // Assert
        //    responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        //    IEnumerable<MusicianProfileDto> result = await DeserializeResponseMessageAsync<IEnumerable<MusicianProfileDto>>(responseMessage);
        //    result.Should().BeEquivalentTo(expectedDto);
        //}

        [Test, Order(100)]
        public async Task Should_Modify()
        {
            // Arrange
            MusicianProfileDto musicianProfileToModify = MusicianProfileDtoData.PerformerProfile;
            var modifyDto = new MusicianProfileModifyBodyDto
            {
                IsMainProfile = false,
                IsDeactivated = false,

                LevelAssessmentPerformer = 1,
                LevelAssessmentStaff = 2,
                ProfilePreferencePerformer = 3,
                ProfilePreferenceStaff = 4,

                BackgroundPerformer = "revised: Background description",
                BackgroundStaff = "revised: Staff-Background description",
                SalaryComment = "revised: Salary only via PayPal, other payments not accepted!",

                PersonId = FakePersons.Performer.Id,
                InstrumentId = SectionSeedData.Alto2.Id,
                QualificationId = SelectValueMappingSeedData.MusicianProfileQualificationMappings[3].Id,
                SalaryId = SelectValueMappingSeedData.MusicianProfileSalaryMappings[2].Id,
                InquiryStatusPerformerId = SelectValueMappingSeedData.MusicianProfileInquiryStatusPerformerMappings[0].Id,
                InquiryStatusStaffId = SelectValueMappingSeedData.MusicianProfileInquiryStatusStaffMappings[2].Id,

                // ToDo Collections
            };

            // Act
            HttpClient client = _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff);

            HttpResponseMessage responseMessage = await client
                .PutAsync(ApiEndpoints.MusicianProfilesController.Put(musicianProfileToModify.Id), BuildStringContent(modifyDto));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Test, Order(1000)]
        public async Task Should_Create()
        {
            // Arrange
            var createDto = new MusicianProfileCreateDto
            {
                PersonId = FakePersons.Performer.Id,
                InstrumentId = SectionSeedData.Euphonium.Id,
                QualificationId = SelectValueMappingSeedData.MusicianProfileQualificationMappings[2].Id,
            };

            var expectedDto = new MusicianProfileDto
            {
                PersonId = createDto.PersonId,
                InstrumentId = createDto.InstrumentId,
                QualificationId = createDto.QualificationId,

                CreatedBy = _staff.DisplayName,
                CreatedAt = FakeDateTime.UtcNow,
                ModifiedAt = null,
                ModifiedBy = null,
            };

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .PostAsync(ApiEndpoints.MusicianProfilesController.Post(), BuildStringContent(createDto));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.Created);
            MusicianProfileDto result = await DeserializeResponseMessageAsync<MusicianProfileDto>(responseMessage);

            result.Should().BeEquivalentTo(expectedDto, opt => opt
                .Excluding(dto => dto.Id));
            result.Id.Should().NotBeEmpty();
        }

        [Test, Order(1000)]
        public async Task Should_Not_Create_Due_To_Missing_Mandatory_Field()
        {
            // Arrange
            var createDto = new MusicianProfileCreateDto
            {
                PersonId = FakePersons.Performer.Id,
                InstrumentId = SectionSeedData.Euphonium.Id,
                // -> this is the missing mandatory field: QualificationId 
            };

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .PostAsync(ApiEndpoints.MusicianProfilesController.Post(), BuildStringContent(createDto));

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
                .DeleteAsync(ApiEndpoints.MusicianProfilesController.Delete(MusicianProfileDtoData.PerformerProfile.Id));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
