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
using Orso.Arpa.Application.Extensions;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Enums;
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
            returnedAppointment.Should().BeEquivalentTo(expectedDto, opt => opt
                .Excluding(dto => dto.CreatedAt)
                .Excluding(dto => dto.Participations)
                .Excluding(dto => dto.Projects));
            returnedAppointment.CreatedAt.Should().NotBeNullOrEmpty();
            for (int i = 0; i < expectedDto.Projects.Count; i++)
            {
                returnedAppointment.Projects[i].Should().BeEquivalentTo(expectedDto.Projects[i], opt => opt.Excluding(p => p.CreatedAt));
            }
        }

        [Test, Order(2)]
        public async Task Should_Get_By_Id()
        {
            AppointmentDto expectedAppointment = AppointmentDtoData.RockingXMasRehearsal;

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_performer)
                .GetAsync(ApiEndpoints.AppointmentsController.Get(expectedAppointment.Id));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            AppointmentDto result = await DeserializeResponseMessageAsync<AppointmentDto>(responseMessage);
            result.Should().BeEquivalentTo(expectedAppointment, opt => opt
                .Excluding(dto => dto.CreatedAt)
                .Excluding(dto => dto.Projects)
                .Excluding(dto => dto.Participations));
            for (int i = 0; i < expectedAppointment.Projects.Count; i++)
            {
                result.Projects[i].Should().BeEquivalentTo(expectedAppointment.Projects[i], opt => opt.Excluding(p => p.CreatedAt));
            }
            for (int i = 0; i < expectedAppointment.Participations.Count; i++)
            {
                result.Participations[i].Should().BeEquivalentTo(expectedAppointment.Participations[i], opt => opt
                    .Excluding(p => p.Person.CreatedAt)
                    .Excluding(p => p.Participation.CreatedAt));
            }
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
                EndTime = DateTime.UtcNow.AddHours(5),
                StartTime = DateTime.UtcNow,
                StatusId = SelectValueMappingSeedData.AppointmentStatusMappings[0].Id
            };

            var expectedDto = new AppointmentDto
            {
                Name = createDto.Name,
                CreatedBy = _staff.DisplayName,
                CreatedAt = DateTime.UtcNow.ToIsoString(),
                ModifiedAt = null,
                ModifiedBy = null,
                InternalDetails = createDto.InternalDetails,
                PublicDetails = createDto.PublicDetails,
                CategoryId = createDto.CategoryId,
                EmolumentId = createDto.EmolumentId,
                EmolumentPatternId = createDto.EmolumentPatternId,
                EndTime = createDto.EndTime.ToIsoString(),
                StartTime = createDto.StartTime.ToIsoString(),
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
            result.CreatedAt.Should().NotBeNullOrEmpty();
        }

        [Test, Order(1001)]
        public async Task Should_Add_Room()
        {
            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .PostAsync(ApiEndpoints.AppointmentsController.AddRoom(
                    AppointmentSeedData.RockingXMasConcert.Id,
                    RoomSeedData.AulaWeiherhofSchule.Id), null);

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test, Order(1002)]
        public async Task Should_Add_Section()
        {
            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .PostAsync(ApiEndpoints.AppointmentsController.AddSection(
                    AppointmentSeedData.RockingXMasConcert.Id,
                    SectionSeedData.Alto.Id), null);

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test, Order(1003)]
        public async Task Should_Add_Project()
        {
            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .PostAsync(ApiEndpoints.AppointmentsController.AddProject(
                    AppointmentSeedData.RockingXMasConcert.Id,
                    ProjectSeedData.RockingXMas.Id), null);

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        private static IEnumerable<TestCaseData> PersonTestData
        {
            get
            {
                yield return new TestCaseData(PersonSeedData.Performer);
                yield return new TestCaseData(PersonSeedData.Admin);
            }
        }

        [Test, Order(1004)]
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

        [Test, Order(100)]
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
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test, Order(101)]
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
                EndTime = DateTime.UtcNow.AddHours(5),
                StartTime = DateTime.UtcNow,
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

        [Test, Order(102)]
        public async Task Should_Set_Dates()
        {
            // Arrange
            Appointment appointmentToModify = AppointmentSeedData.RockingXMasRehearsal;
            var setDatesDto = new AppointmentSetDatesDto
            {
                StartTime = DateTime.UtcNow,
                EndTime = DateTime.UtcNow.AddHours(5)
            };

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .PutAsync(ApiEndpoints.AppointmentsController.SetDates(appointmentToModify.Id), BuildStringContent(setDatesDto));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Test, Order(10001)]
        public async Task Should_Remove_Room()
        {
            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .DeleteAsync(ApiEndpoints.AppointmentsController.AddRoom(
                    AppointmentSeedData.AfterShowParty.Id,
                    RoomSeedData.AulaWeiherhofSchule.Id));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Test, Order(10002)]
        public async Task Should_Remove_Section()
        {
            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .DeleteAsync(ApiEndpoints.AppointmentsController.AddSection(
                    AppointmentSeedData.AfterShowParty.Id,
                    SectionSeedData.Alto.Id));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Test, Order(10003)]
        public async Task Should_Remove_Project()
        {
            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .DeleteAsync(ApiEndpoints.AppointmentsController.AddProject(
                    AppointmentSeedData.AfterShowParty.Id,
                    ProjectSeedData.RockingXMas.Id));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Test, Order(10004)]
        public async Task Should_Delete()
        {
            // Arrange
            Appointment appointmentToDelete = AppointmentSeedData.AfterShowParty;

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .DeleteAsync(ApiEndpoints.AppointmentsController.Delete(appointmentToDelete.Id));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
