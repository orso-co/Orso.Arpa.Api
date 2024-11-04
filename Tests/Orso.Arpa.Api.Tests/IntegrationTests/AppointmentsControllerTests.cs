using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Ical.Net;
using Ical.Net.DataTypes;
using Microsoft.AspNetCore.Mvc;
using netDumbster.smtp;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Application.AppointmentApplication.Model;
using Orso.Arpa.Application.AppointmentParticipationApplication.Model;
using Orso.Arpa.Application.MusicianProfileApplication.Model;
using Orso.Arpa.Domain.AppointmentDomain.Enums;
using Orso.Arpa.Domain.AppointmentDomain.Model;
using Orso.Arpa.Domain.PersonDomain.Model;
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
                yield return new TestCaseData(DateRange.Day, new DateTime(2019, 12, 21), new List<AppointmentListDto> {
                    AppointmentListDtoData.RockingXMasRehearsal,
                    AppointmentListDtoData.RehearsalWeekend
                });
                // 16.-22.12.2019
                yield return new TestCaseData(DateRange.Week, new DateTime(2019, 12, 21), new List<AppointmentListDto> {
                    AppointmentListDtoData.RockingXMasRehearsal,
                    AppointmentListDtoData.AppointmentWithoutProject,
                    AppointmentListDtoData.RehearsalWeekend
                });
                yield return new TestCaseData(DateRange.Month, new DateTime(2020, 12, 21), new List<AppointmentListDto> {
                    AppointmentListDtoData.AuditionDays,
                    AppointmentListDtoData.PhotoSession,
                    AppointmentListDtoData.StaffMeeting
                });
            }
        }

        [TestCaseSource(nameof(s_appointmentQueryTestData))]
        [Test, Order(1)]
        public async Task Should_Get_Appointments(
            DateRange dateRange,
            DateTime date,
            IList<AppointmentListDto> expectedDtos)
        {
            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .GetAsync(ApiEndpoints.AppointmentsController.Get(date, dateRange));

            // Assert
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            IEnumerable<AppointmentListDto> result = await DeserializeResponseMessageAsync<IEnumerable<AppointmentListDto>>(responseMessage);

            _ = result.Should().BeEquivalentTo(expectedDtos);
        }

        private static IEnumerable<TestCaseData> s_appointmentByIdQueryTestData
        {
            get
            {
                yield return new TestCaseData(AppointmentDtoData.RockingXMasRehearsal);
                yield return new TestCaseData(AppointmentDtoData.RockingXMasConcert);
                yield return new TestCaseData(AppointmentDtoData.AfterShowParty);
                yield return new TestCaseData(AppointmentDtoData.StaffMeeting);
                yield return new TestCaseData(AppointmentDtoData.PhotoSession);
                yield return new TestCaseData(AppointmentDtoData.RehearsalWeekend);
                yield return new TestCaseData(AppointmentDtoData.AuditionDays);
                yield return new TestCaseData(AppointmentDtoData.AltoRehearsal);
                yield return new TestCaseData(AppointmentDtoData.SopranoRehearsal);
            }
        }

        [Test, Order(2)]
        [TestCaseSource(nameof(s_appointmentByIdQueryTestData))]
        public async Task Should_Get_By_Id_With_Participations(AppointmentDto expectedDto)
        {
            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(FakeUsers.Staff)
                .GetAsync(ApiEndpoints.AppointmentsController.Get(expectedDto.Id, true));

            // Assert
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            AppointmentDto result = await DeserializeResponseMessageAsync<AppointmentDto>(responseMessage);
            _ = result.Should().BeEquivalentTo(expectedDto);
        }

        [Test, Order(3)]
        [TestCaseSource(nameof(s_appointmentByIdQueryTestData))]
        public async Task Should_Get_By_Id_Without_Participations(AppointmentDto expectedDto)
        {
            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(FakeUsers.Staff)
                .GetAsync(ApiEndpoints.AppointmentsController.Get(expectedDto.Id, false));

            // Assert
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            AppointmentDto result = await DeserializeResponseMessageAsync<AppointmentDto>(responseMessage);
            _ = result.Should().BeEquivalentTo(expectedDto, opt => opt.Excluding(dto => dto.Participations));
            _ = result.Participations.Should().BeEmpty();
        }


        [Test]
        [Order(4)]
        public async Task Should_Export_Appointments_To_Ics()
        {
            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .GetAsync(ApiEndpoints.AppointmentsController.ExportToIcs());

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            responseMessage.Content.Headers.ContentType.ToString().Should().Be("text/calendar");

            string icsContent = await responseMessage.Content.ReadAsStringAsync();
            icsContent.Should().NotBeNullOrEmpty();


            icsContent.Should().Contain("BEGIN:VEVENT");
            icsContent.Should().Contain("END:VEVENT");

            icsContent.Should().Contain("SUMMARY:");
            icsContent.Should().Contain("DTSTART:");
            icsContent.Should().Contain("DTEND:");
            icsContent.Should().Contain("DESCRIPTION:");

            icsContent.Should().Contain("BEGIN:VTIMEZONE");
            icsContent.Should().Contain("TZID:Europe/Berlin");

            var calendar = Calendar.Load(icsContent);
            calendar.Events.Should().NotBeEmpty();
            var firstEvent = calendar.Events.First();
            firstEvent.Summary.Should().NotBeNullOrEmpty();
            firstEvent.Start.Should().BeOfType<CalDateTime>();
            firstEvent.End.Should().BeOfType<CalDateTime>();
            ((CalDateTime)firstEvent.Start).Value.Should().BeAfter(DateTime.MinValue);
            ((CalDateTime)firstEvent.End).Value.Should().BeAfter(((CalDateTime)firstEvent.Start).Value);
        }


        [Test, Order(5)]
        public async Task Should_Send_Appointment_Changed_Notification() {
            // Arrange
            _fakeSmtpServer.ClearReceivedEmail();
            IEnumerable<string> expectedToAddresses = [
                "arpa@test.smtp"
            ];
            IEnumerable<string> expectedBccAddresses = [
                UserSeedData.Admin.Email,
                UserTestSeedData.UserWithoutRole.Email,
                UserTestSeedData.Staff.Email,
                UserTestSeedData.Performer.Email
            ];

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .PostAsync(ApiEndpoints.AppointmentsController.SendAppointmentChangedNotification(
                    AppointmentSeedData.PhotoSession.Id,
                    true), null);

            // Assert
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
            _fakeSmtpServer.ReceivedEmailCount.Should().Be(1);
            SmtpMessage sentEmail = _fakeSmtpServer.ReceivedEmail[0];
            sentEmail.ToAddresses.Select(a => a.Address).Should().BeEquivalentTo(expectedToAddresses, opt => opt.WithoutStrictOrdering());
            sentEmail.BccAddresses.Select(a => a.Address).Should().BeEquivalentTo(expectedBccAddresses, opt => opt.WithoutStrictOrdering());
            sentEmail.Subject.Should().Be("Ein Termin in ARPA wurde aktualisiert!");
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
                SalaryId = Guid.Parse("88da1c17-9efc-4f69-ba0f-39c76592845b"),
                Status = AppointmentStatus.Scheduled
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
                Status = createDto.Status,
                SalaryId = createDto.SalaryId
            };

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .PostAsync(ApiEndpoints.AppointmentsController.Post(), BuildStringContent(createDto));

            // Assert
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.Created);

            AppointmentDto result = await DeserializeResponseMessageAsync<AppointmentDto>(responseMessage);

            _ = result.Should().BeEquivalentTo(expectedDto, opt => opt.Excluding(r => r.Id));
            _ = result.Id.Should().NotBeEmpty();
            _ = responseMessage.Headers.Location.AbsolutePath.Should().Be($"/{ApiEndpoints.AppointmentsController.Get(result.Id)}");
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
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Test, Order(100)]
        public async Task Should_Add_Section()
        {
            AppointmentDto expectedDto = AppointmentDtoData.RockingXMasRehearsal;
            expectedDto.Participations.RemoveAt(1);
            expectedDto.Participations.RemoveAt(1); // the second item has already been removed so the third item is on index pos. 1 now
            expectedDto.Sections.Add(SectionDtoData.Alto);

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .PostAsync(ApiEndpoints.AppointmentsController.Section(
                    AppointmentSeedData.RockingXMasRehearsal.Id,
                    SectionSeedData.Alto.Id), null);

            // Assert
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            AppointmentDto result = await DeserializeResponseMessageAsync<AppointmentDto>(responseMessage);
            _ = result.Should().BeEquivalentTo(expectedDto);
        }

        [Test, Order(101)]
        public async Task Should_Add_Project()
        {
            // Arrange
            AppointmentDto expectedDto = AppointmentDtoData.RockingXMasConcert;
            expectedDto.Participations.Clear();
            expectedDto.Participations.Add(AppointmentDtoData.PerformerParticipation);
            expectedDto.Projects.Add(ProjectDtoData.HoorayForHollywood);

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .PostAsync(ApiEndpoints.AppointmentsController.Project(
                    AppointmentSeedData.AppointmentWithoutProject.Id,
                    ProjectSeedData.HoorayForHollywood.Id), null);

            // Assert
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            AppointmentDto result = await DeserializeResponseMessageAsync<AppointmentDto>(responseMessage);
            _ = result.Should().BeEquivalentTo(expectedDto);
        }

        private static IEnumerable<TestCaseData> PersonTestData
        {
            get
            {
                yield return new TestCaseData(PersonTestSeedData.Performer, HttpStatusCode.NoContent);
                yield return new TestCaseData(PersonTestSeedData.LockedOutUser, HttpStatusCode.Forbidden);
            }
        }

        private static readonly string[] s_appointmentNotFoundMessage = ["Appointment could not be found."];


        [Test, Order(105)]
        [TestCaseSource(nameof(PersonTestData))]
        public async Task Should_Set_Participation_Result(Person person, HttpStatusCode expectedStatusCode)
        {
            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .PutAsync(ApiEndpoints.AppointmentsController.SetParticipationResult(
                    AppointmentSeedData.RockingXMasRehearsal.Id,
                    person.Id), BuildStringContent(new AppointmentParticipationSetResultBodyDto { Result = AppointmentParticipationResult.AwaitingScan }));

            // Assert
            _ = responseMessage.StatusCode.Should().Be(expectedStatusCode);
        }

        [Test, Order(106)]
        public async Task Should_Set_Venue()
        {
            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .PutAsync(ApiEndpoints.AppointmentsController.SetVenue(
                    AppointmentSeedData.AppointmentWithoutProject.Id,
                    VenueSeedData.WeiherhofSchule.Id), null);

            // Assert
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Test, Order(107)]
        public async Task Should_Modify()
        {
            // Arrange
            Appointment appointmentToModify = AppointmentSeedData.AppointmentWithoutProject;

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
                Status = AppointmentStatus.Confirmed
            };

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .PutAsync(ApiEndpoints.AppointmentsController.Put(appointmentToModify.Id), BuildStringContent(modifyDto));

            // Assert
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Test, Order(108)]
        public async Task Should_Modify_With_Only_Mandatory_Fields_Specified()
        {
            // Arrange
            Appointment appointmentToModify = AppointmentSeedData.AppointmentWithoutProject;

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
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Test, Order(109)]
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
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.NotFound);
            ValidationProblemDetails errorMessage = await DeserializeResponseMessageAsync<ValidationProblemDetails>(responseMessage);
            _ = errorMessage.Title.Should().Be("Resource not found.");
            _ = errorMessage.Status.Should().Be(404);
            _ = errorMessage.Errors.Should().BeEquivalentTo(new Dictionary<string, string[]>() { { "Id", s_appointmentNotFoundMessage } });
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

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .PutAsync(ApiEndpoints.AppointmentsController.SetDates(appointmentToModify.Id), BuildStringContent(setDatesDto));

            // Assert
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            AppointmentDto result = await DeserializeResponseMessageAsync<AppointmentDto>(responseMessage);
            _ = result.Should().BeEquivalentTo(expectedDto);
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
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Test, Order(102)]
        public async Task Should_Remove_Section()
        {
            // Arrange
            AppointmentDto expectedDto = AppointmentDtoData.AfterShowParty;
            expectedDto.Sections.Clear();
            expectedDto.Participations.Add(new AppointmentParticipationListItemDto
            {
                Person = ReducedPersonDtoData.Staff,
                MusicianProfiles = new List<ReducedMusicianProfileDto>
                    {
                        ReducedMusicianProfileDtoData.StaffProfile1,
                        ReducedMusicianProfileDtoData.StaffProfile2
                    }
            });
            expectedDto.Participations.Add(new AppointmentParticipationListItemDto
            {
                Person = ReducedPersonDtoData.Admin,
                MusicianProfiles = new List<ReducedMusicianProfileDto>
                    {
                        ReducedMusicianProfileDtoData.AdminProfile1
                    }
            });

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .DeleteAsync(ApiEndpoints.AppointmentsController.Section(
                    AppointmentSeedData.AfterShowParty.Id,
                    SectionSeedData.Alto.Id));

            // Assert
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            AppointmentDto result = await DeserializeResponseMessageAsync<AppointmentDto>(responseMessage);
            _ = result.Should().BeEquivalentTo(expectedDto);
        }

        [Test, Order(103)]
        public async Task Should_Remove_Project()
        {
            // Arrange
            AppointmentDto expectedDto = AppointmentDtoData.StaffMeeting;
            expectedDto.Projects.Clear();
            expectedDto.Participations.Clear();

            AppointmentParticipationListItemDto performerParticipation = AppointmentDtoData.PerformerParticipationRockingXMasRehearsal;
            performerParticipation.MusicianProfiles.Add(ReducedMusicianProfileDtoData.PerformerHornProfile);
            performerParticipation.MusicianProfiles.Add(ReducedMusicianProfileDtoData.PerformerDeactivatedTubaProfile);
            performerParticipation.Participation = null;
            expectedDto.Participations.Add(performerParticipation);

            AppointmentParticipationListItemDto staffParticipation = AppointmentDtoData.StaffParticipation;
            staffParticipation.Participation = null;
            expectedDto.Participations.Add(staffParticipation);

            AppointmentParticipationListItemDto adminParticipation = AppointmentDtoData.AdminParticipation;
            adminParticipation.MusicianProfiles.Add(ReducedMusicianProfileDtoData.AdminProfile2);
            expectedDto.Participations.Add(adminParticipation);

            expectedDto.Participations.Add(AppointmentDtoData.WithoutRoleParticipation);

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .DeleteAsync(ApiEndpoints.AppointmentsController.Project(
                    AppointmentSeedData.StaffMeeting.Id,
                    ProjectSeedData.HoorayForHollywood.Id));

            // Assert
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            AppointmentDto result = await DeserializeResponseMessageAsync<AppointmentDto>(responseMessage);
            _ = result.Should().BeEquivalentTo(expectedDto);
        }

        [Test, Order(118)]
        public async Task Should_Set_New_Participation_Prediction()
        {
            // Arrange
            var dto = new AppointmentParticipationSetPredictionBodyDto
            {
                CommentByPerformerInner = "CommentByPerformerInner",
                Prediction = AppointmentParticipationPrediction.Partly
            };

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .PutAsync(ApiEndpoints.AppointmentsController.SetParticipationPrediction(
                    AppointmentSeedData.PhotoSession.Id,
                    PersonSeedData.AdminPersonId),
                    BuildStringContent(dto));

            // Assert
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Test, Order(119)]
        public async Task Should_Set_Existing_Participation_Prediction()
        {
            // Arrange
            var dto = new AppointmentParticipationSetPredictionBodyDto
            {
                CommentByPerformerInner = "CommentByPerformerInner",
                Prediction = AppointmentParticipationPrediction.Yes
            };

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .PutAsync(ApiEndpoints.AppointmentsController.SetParticipationPrediction(
                    AppointmentSeedData.RockingXMasRehearsal.Id,
                    _performer.PersonId),
                    BuildStringContent(dto));

            // Assert
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Test, Order(10004)]
        public async Task Should_Delete()
        {
            // Arrange
            Appointment appointmentToDelete = AppointmentSeedData.StaffMeeting;

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_admin)
                .DeleteAsync(ApiEndpoints.AppointmentsController.Delete(appointmentToDelete.Id));

            // Assert
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);

            HttpResponseMessage getResponseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .GetAsync(ApiEndpoints.AppointmentsController.Get(appointmentToDelete.Id));
            _ = getResponseMessage.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
