using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Application.AppointmentApplication;
using Orso.Arpa.Application.SectionApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Enums;
using Orso.Arpa.Misc;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.DtoTestData;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Api.Tests.IntegrationTests
{
    [TestFixture]
    public class AppointmentsControllerTests : IntegrationTestBase
    {
        [Test, Order(1)]
        public async Task Should_Get_Appointments()
        {
            // Act
                HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_performer)
                .GetAsync(ApiEndpoints.AppointmentsController.Get(new DateTime(2019, 12, 21), DateRange.Day));
                AppointmentDto expectedDto = AppointmentDtoData.RockingXMasRehearsal;

                // Assert
                responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
                IEnumerable<AppointmentDto> result = await DeserializeResponseMessageAsync<IEnumerable<AppointmentDto>>(responseMessage);
                result.Count().Should().Be(1);
                AppointmentDto returnedAppointment = result.First();
                returnedAppointment.Should().BeEquivalentTo(expectedDto);
        }

        [Test, Order(2)]
        public async Task Should_Get_By_Id()
        {
            using var context = new DateTimeProviderContext(new DateTime(2021, 1, 1));
            AppointmentDto expectedAppointment = AppointmentDtoData.RockingXMasRehearsal;

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_performer)
                .GetAsync(ApiEndpoints.AppointmentsController.Get(expectedAppointment.Id));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            AppointmentDto result = await DeserializeResponseMessageAsync<AppointmentDto>(responseMessage);
            result.Should().BeEquivalentTo(expectedAppointment);
        }

        [Test, Order(1000)]
        public async Task Should_Create()
        {
            // Arrange
            var createDto = new AppointmentCreateDto
            {
                Name = "New Appointment",
                InternalDetails = "Internal Details",
                PublicDetails = "Public Details",
                CategoryId = SelectValueMappingSeedData.AppointmentCategoryMappings[0].Id,
                EmolumentId = SelectValueMappingSeedData.AppointmentEmolumentMappings[0].Id,
                EmolumentPatternId = SelectValueMappingSeedData.AppointmentEmolumentPatternMappings[0].Id,
                EndTime = new DateTime(2021, 3, 5, 14, 15, 20),
                StartTime = new DateTime(2021,3,5,9,15,20),
                StatusId = SelectValueMappingSeedData.AppointmentStatusMappings[0].Id
            };

            var expectedDto = new AppointmentDto
            {
                Name = createDto.Name,
                CreatedBy = _staff.DisplayName,
                CreatedAt = DateTimeProvider.Instance.GetUtcNow(),
                ModifiedAt = null,
                ModifiedBy = null,
                InternalDetails = createDto.InternalDetails,
                PublicDetails = createDto.PublicDetails,
                CategoryId = createDto.CategoryId,
                EmolumentId = createDto.EmolumentId,
                EmolumentPatternId = createDto.EmolumentPatternId,
                EndTime = createDto.EndTime,
                StartTime = createDto.StartTime,
                StatusId = createDto.StatusId
            };

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .PostAsync(ApiEndpoints.AppointmentsController.Post(), BuildStringContent(createDto));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.Created);
            AppointmentDto result = await DeserializeResponseMessageAsync<AppointmentDto>(responseMessage);

            result.Should().BeEquivalentTo(expectedDto, opt => opt.Excluding(r => r.Id).Excluding(r => r.CreatedAt));
            result.Id.Should().NotBeEmpty();
            result.CreatedAt.Should().BeAfter(DateTime.MinValue);
        }

        [Test, Order(104)]
        public async Task Should_Add_Room()
        {
            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .PostAsync(ApiEndpoints.AppointmentsController.Room(
                    AppointmentSeedData.RockingXMasRehearsal.Id,
                    RoomSeedData.AulaWeiherhofSchule.Id), null);

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Test, Order(100)]
        public async Task Should_Add_Section()
        {
            AppointmentDto expectedDto = AppointmentDtoData.RockingXMasRehearsal;
            expectedDto.Sections.Add(new SectionDto() { Id = Guid.Parse("a06431be-f9d6-44dc-8fdb-fbf8aa2bb940"), Name = "Alto" });

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .PostAsync(ApiEndpoints.AppointmentsController.Section(
                    AppointmentSeedData.RockingXMasRehearsal.Id,
                    SectionSeedData.Alto.Id), null);

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            AppointmentDto result = await DeserializeResponseMessageAsync<AppointmentDto>(responseMessage);
            result.Should().BeEquivalentTo(expectedDto);
        }

        [Test, Order(101)]
        public async Task Should_Add_Project()
        {
            // Arrange
            AppointmentDto expectedDto = AppointmentDtoData.RockingXMasConcert;
            expectedDto.Projects.Add(ProjectDtoData.HoorayForHollywood);

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .PostAsync(ApiEndpoints.AppointmentsController.Project(
                    AppointmentSeedData.RockingXMasConcert.Id,
                    ProjectSeedData.HoorayForHollywood.Id), null);

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            AppointmentDto result = await DeserializeResponseMessageAsync<AppointmentDto>(responseMessage);
            result.Should().BeEquivalentTo(expectedDto);
        }

        private static IEnumerable<TestCaseData> PersonTestData
        {
            get
            {
                yield return new TestCaseData(PersonTestSeedData.Performer);
                yield return new TestCaseData(PersonSeedData.Admin);
            }
        }

        [Test, Order(105)]
        [TestCaseSource(nameof(PersonTestData))]
        public async Task Should_Set_Participation_Result(Person person)
        {
            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .PutAsync(ApiEndpoints.AppointmentsController.SetParticipationResult(
                    AppointmentSeedData.RockingXMasRehearsal.Id,
                    person.Id,
                    SelectValueMappingSeedData.AppointmentParticipationResultMappings[0].Id), null);

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Test, Order(106)]
        public async Task Should_Set_Venue()
        {
            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .PutAsync(ApiEndpoints.AppointmentsController.SetVenue(
                    AppointmentSeedData.RockingXMasConcert.Id,
                    VenueSeedData.WeiherhofSchule.Id), null);

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Test, Order(107)]
        public async Task Should_Modify()
        {
            // Arrange
            Appointment appointmentToModify = AppointmentSeedData.RockingXMasConcert;

            var modifyDto = new AppointmentModifyDto
            {
                Name = "New Appointment",
                InternalDetails = "Internal Details",
                PublicDetails = "Public Details",
                CategoryId = SelectValueMappingSeedData.AppointmentCategoryMappings[0].Id,
                EmolumentId = SelectValueMappingSeedData.AppointmentEmolumentMappings[0].Id,
                EmolumentPatternId = SelectValueMappingSeedData.AppointmentEmolumentPatternMappings[0].Id,
                EndTime = DateTimeProvider.Instance.GetUtcNow().AddHours(5),
                StartTime = DateTimeProvider.Instance.GetUtcNow(),
                StatusId = SelectValueMappingSeedData.AppointmentStatusMappings[0].Id
            };

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .PutAsync(ApiEndpoints.AppointmentsController.Put(appointmentToModify.Id), BuildStringContent(modifyDto));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Test, Order(108)]
        public async Task Should_Set_Dates()
        {
            // Arrange
            Appointment appointmentToModify = AppointmentSeedData.PhotoSession;
            var setDatesDto = new AppointmentSetDatesDto
            {
                StartTime = DateTimeProvider.Instance.GetUtcNow(),
                EndTime = DateTimeProvider.Instance.GetUtcNow().AddHours(5)
            };
            AppointmentDto expectedDto = AppointmentDtoData.PhotoSession;
            expectedDto.EndTime = setDatesDto.EndTime.Value;
            expectedDto.StartTime = setDatesDto.StartTime.Value;
            expectedDto.ModifiedBy = _staff.DisplayName;

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .PutAsync(ApiEndpoints.AppointmentsController.SetDates(appointmentToModify.Id), BuildStringContent(setDatesDto));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            AppointmentDto result = await DeserializeResponseMessageAsync<AppointmentDto>(responseMessage);
            result.Should().BeEquivalentTo(expectedDto, opt => opt.Excluding(dto => dto.ModifiedAt));
            result.ModifiedAt.Should().BeCloseTo(DateTime.UtcNow, 10000);
        }

        [Test, Order(109)]
        public async Task Should_Remove_Room()
        {
            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .DeleteAsync(ApiEndpoints.AppointmentsController.Room(
                    AppointmentSeedData.AfterShowParty.Id,
                    RoomSeedData.AulaWeiherhofSchule.Id));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Test, Order(102)]
        public async Task Should_Remove_Section()
        {
            // Arrange
            AppointmentDto expectedDto = AppointmentDtoData.AfterShowParty;
            expectedDto.Sections.Clear();

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .DeleteAsync(ApiEndpoints.AppointmentsController.Section(
                    AppointmentSeedData.AfterShowParty.Id,
                    SectionSeedData.Alto.Id));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            AppointmentDto result = await DeserializeResponseMessageAsync<AppointmentDto>(responseMessage);
            result.Should().BeEquivalentTo(expectedDto);
        }

        [Test, Order(103)]
        public async Task Should_Remove_Project()
        {
            // Arrange
            AppointmentDto expectedDto = AppointmentDtoData.StaffMeeting;
            expectedDto.Projects.Clear();
            expectedDto.Participations.Clear();

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .DeleteAsync(ApiEndpoints.AppointmentsController.Project(
                    AppointmentSeedData.StaffMeeting.Id,
                    ProjectSeedData.HoorayForHollywood.Id));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            AppointmentDto result = await DeserializeResponseMessageAsync<AppointmentDto>(responseMessage);
            result.Should().BeEquivalentTo(expectedDto);
        }

        [Test, Order(10004)]
        public async Task Should_Delete()
        {
            // Arrange
            Appointment appointmentToDelete = AppointmentSeedData.StaffMeeting;
            HttpClient client = _authenticatedServer.CreateClient().AuthenticateWith(_staff);

            // Act
            HttpResponseMessage responseMessage = await client
                .DeleteAsync(ApiEndpoints.AppointmentsController.Delete(appointmentToDelete.Id));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);

            HttpResponseMessage getResponseMessage = await client
                .GetAsync(ApiEndpoints.AppointmentsController.Get(appointmentToDelete.Id));
            getResponseMessage.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
