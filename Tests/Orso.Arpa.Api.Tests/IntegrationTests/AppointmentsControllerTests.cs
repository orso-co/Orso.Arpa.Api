using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Application.AppointmentApplication;
using Orso.Arpa.Application.AppointmentParticipationApplication;
using Orso.Arpa.Application.MusicianProfileApplication;
using Orso.Arpa.Application.SectionApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Enums;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.DtoTestData;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Api.Tests.IntegrationTests
{
    [TestFixture]
    public class AppointmentsControllerTests : IntegrationTestBase
    {
        private static IEnumerable<TestCaseData> s_appointmentQueryTestData
        {
            get
            {
                yield return new TestCaseData(DateRange.Day, new DateTime(2019, 12, 21), new List<AppointmentDto> {
                    AppointmentDtoData.RockingXMasRehearsalForPerformer,
                    AppointmentDtoData.RehearsalWeekend
                }, false);
                // 16.-22.12.2019
                yield return new TestCaseData(DateRange.Week, new DateTime(2019, 12, 21), new List<AppointmentDto> {
                    AppointmentDtoData.RockingXMasRehearsalForPerformer,
                    AppointmentDtoData.RockingXMasConcertForPerformer,
                    AppointmentDtoData.RehearsalWeekend
                }, true);
                yield return new TestCaseData(DateRange.Month, new DateTime(2020, 12, 21), new List<AppointmentDto> {
                    AppointmentDtoData.AuditionDays,
                    AppointmentDtoData.PhotoSession,
                    AppointmentDtoData.StaffMeetingForPerformer
                }, true);
            }
        }

        [TestCaseSource(nameof(s_appointmentQueryTestData))]
        [Test, Order(1)]
        public async Task Should_Get_Appointments(DateRange dateRange, DateTime date, IList<AppointmentDto> expectedDtos, bool excludeParticipations)
        {
            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_performer)
                .GetAsync(ApiEndpoints.AppointmentsController.Get(date, dateRange));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            IEnumerable<AppointmentDto> result = await DeserializeResponseMessageAsync<IEnumerable<AppointmentDto>>(responseMessage);
            if (excludeParticipations)
            {
                result.Should().BeEquivalentTo(expectedDtos, opt => opt.Excluding(dto => dto.Participations));
            }
            else
            {
                result.Should().BeEquivalentTo(expectedDtos);
            }
        }

        private static IEnumerable<TestCaseData> s_appointmentByIdTestData
        {
            get
            {
                yield return new TestCaseData(FakeUsers.Performer, AppointmentDtoData.RockingXMasRehearsalForPerformer);
                yield return new TestCaseData(FakeUsers.Staff, AppointmentDtoData.RockingXMasRehearsalForStaff);
            }
        }

        [Test, Order(2)]
        [TestCaseSource(nameof(s_appointmentByIdTestData))]
        public async Task Should_Get_By_Id(User userToLogin, AppointmentDto expectedDto)
        {
            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(userToLogin)
                .GetAsync(ApiEndpoints.AppointmentsController.Get(expectedDto.Id));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            AppointmentDto result = await DeserializeResponseMessageAsync<AppointmentDto>(responseMessage);
            result.Should().BeEquivalentTo(expectedDto);
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
                EndTime = new DateTime(2021, 3, 5, 14, 15, 20),
                StartTime = new DateTime(2021, 3, 5, 9, 15, 20),
                SalaryId = Guid.Parse("88da1c17-9efc-4f69-ba0f-39c76592845b")
            };

            var expectedDto = new AppointmentDto
            {
                Name = createDto.Name,
                CreatedBy = _staff.DisplayName,
                CreatedAt = FakeDateTime.UtcNow,
                ModifiedAt = null,
                ModifiedBy = null,
                InternalDetails = createDto.InternalDetails,
                PublicDetails = createDto.PublicDetails,
                EndTime = createDto.EndTime,
                StartTime = createDto.StartTime,
                StatusId = createDto.StatusId,
                SalaryId = createDto.SalaryId
            };

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .PostAsync(ApiEndpoints.AppointmentsController.Post(), BuildStringContent(createDto));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.Created);

            AppointmentDto result = await DeserializeResponseMessageAsync<AppointmentDto>(responseMessage);

            result.Should().BeEquivalentTo(expectedDto, opt => opt.Excluding(r => r.Id));
            result.Id.Should().NotBeEmpty();
            responseMessage.Headers.Location.AbsolutePath.Should().Be($"/{ApiEndpoints.AppointmentsController.Get(result.Id)}");
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
            AppointmentDto expectedDto = AppointmentDtoData.RockingXMasRehearsalForStaff;
            expectedDto.Participations.RemoveAt(1);
            expectedDto.Participations.RemoveAt(1);
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

            var modifyDto = new AppointmentModifyBodyDto
            {
                Name = "New Appointment",
                InternalDetails = "Internal Details",
                PublicDetails = "Public Details",
                CategoryId = SelectValueMappingSeedData.AppointmentCategoryMappings[0].Id,
                SalaryId = SelectValueMappingSeedData.AppointmentSalaryMappings[0].Id,
                SalaryPatternId = SelectValueMappingSeedData.AppointmentSalaryPatternMappings[0].Id,
                EndTime = FakeDateTime.UtcNow.AddHours(5),
                StartTime = FakeDateTime.UtcNow,
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
        public async Task Should_Modify_With_Only_Mandatory_Fields_Specified()
        {
            // Arrange
            Appointment appointmentToModify = AppointmentSeedData.RockingXMasConcert;

            var modifyDto = new AppointmentModifyBodyDto
            {
                Name = "New Appointment",
                InternalDetails = "Internal Details",
                PublicDetails = "Public Details",
                EndTime = FakeDateTime.UtcNow.AddHours(5),
                StartTime = FakeDateTime.UtcNow,
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
        public async Task Should_Not_Modify_If_Not_Existing_Id_Is_Supplied()
        {
            // Arrange
            var modifyDto = new AppointmentModifyBodyDto
            {
                Name = "New Appointment",
                InternalDetails = "Internal Details",
                PublicDetails = "Public Details",
                EndTime = FakeDateTime.UtcNow.AddHours(5),
                StartTime = FakeDateTime.UtcNow,
            };

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .PutAsync(ApiEndpoints.AppointmentsController.Put(Guid.NewGuid()), BuildStringContent(modifyDto));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NotFound);
            ValidationProblemDetails errorMessage = await DeserializeResponseMessageAsync<ValidationProblemDetails>(responseMessage);
            errorMessage.Title.Should().Be("Resource not found.");
            errorMessage.Status.Should().Be(404);
            errorMessage.Errors.Should().BeEquivalentTo(new Dictionary<string, string[]>() { { "Id", new[] { "Appointment could not be found." } } });
        }

        [Test, Order(108)]
        public async Task Should_Set_Dates()
        {
            // Arrange
            Appointment appointmentToModify = AppointmentSeedData.PhotoSession;
            var setDatesDto = new AppointmentSetDatesBodyDto
            {
                StartTime = FakeDateTime.UtcNow,
                EndTime = FakeDateTime.UtcNow.AddHours(5)
            };
            AppointmentDto expectedDto = AppointmentDtoData.PhotoSession;
            expectedDto.EndTime = setDatesDto.EndTime.Value;
            expectedDto.StartTime = setDatesDto.StartTime.Value;
            expectedDto.ModifiedBy = _staff.DisplayName;
            expectedDto.ModifiedAt = FakeDateTime.UtcNow;
            AppointmentParticipationListItemDto performerParticipation = AppointmentDtoData.PerformerParticipation;
            performerParticipation.Participation = null;
            expectedDto.Participations.Add(performerParticipation);
            AppointmentParticipationListItemDto staffParticipation = AppointmentDtoData.StaffParticipation;
            staffParticipation.Participation = null;
            expectedDto.Participations.Add(staffParticipation);
            AppointmentParticipationListItemDto adminParticipation = AppointmentDtoData.AdminParticipation;
            adminParticipation.MusicianProfiles.Add(new ReducedMusicianProfileDto
            {
                Id = Guid.Parse("9f6f3cab-6b0d-463e-8d66-58b9c760d498"),
                InstrumentName = SectionSeedData.Soprano2.Name
            });
            expectedDto.Participations.Add(adminParticipation);
            expectedDto.Participations.Add(AppointmentDtoData.WithoutRoleParticipation);

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .PutAsync(ApiEndpoints.AppointmentsController.SetDates(appointmentToModify.Id), BuildStringContent(setDatesDto));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            AppointmentDto result = await DeserializeResponseMessageAsync<AppointmentDto>(responseMessage);
            result.Should().BeEquivalentTo(expectedDto);
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
            AppointmentDto expectedDto = AppointmentDtoData.AfterShowPartyForStaff;
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
            AppointmentParticipationListItemDto performerParticipation = AppointmentDtoData.PerformerParticipation;
            performerParticipation.MusicianProfiles.Add(new ReducedMusicianProfileDto
            {
                Id = Guid.Parse("e2ef2e6c-035e-4fff-9293-a6a7b67524a9"),
                InstrumentName = "Horn",
                Qualification = "Student"
            });
            performerParticipation.MusicianProfiles.Add(new ReducedMusicianProfileDto
            {
                Id = Guid.Parse("056a27f0-cd88-4cd9-8729-ce2f23b8b0ef"),
                InstrumentName = "Tuba"
            });
            performerParticipation.Participation = null;
            expectedDto.Participations.Add(performerParticipation);
            AppointmentParticipationListItemDto staffParticipation = AppointmentDtoData.StaffParticipation;
            staffParticipation.Participation = null;
            expectedDto.Participations.Add(staffParticipation);
            AppointmentParticipationListItemDto adminParticipation = AppointmentDtoData.AdminParticipation;
            expectedDto.Participations.Add(adminParticipation);
            adminParticipation.MusicianProfiles.Add(new ReducedMusicianProfileDto
            {
                Id = Guid.Parse("9f6f3cab-6b0d-463e-8d66-58b9c760d498"),
                InstrumentName = "Soprano 2"
            });
            expectedDto.Participations.Add(AppointmentDtoData.WithoutRoleParticipation);

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
            HttpClient client = _authenticatedServer.CreateClient().AuthenticateWith(_admin);

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
