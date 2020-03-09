using System;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using MediatR;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Application.AppointmentApplication;
using Orso.Arpa.Application.AppointmentParticipationApplication;
using Orso.Arpa.Application.Services;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Tests.Shared.DtoTestData;
using Orso.Arpa.Tests.Shared.TestSeedData;
using AppointmentParticipations = Orso.Arpa.Domain.Logic.AppointmentParticipations;
using Appointments = Orso.Arpa.Domain.Logic.Appointments;

namespace Orso.Arpa.Application.Tests.ServiceTests
{
    [TestFixture]
    public class AppointmentServiceTests
    {
        private IMediator _subMediator;
        private IMapper _subMapper;

        [SetUp]
        public void SetUp()
        {
            _subMediator = Substitute.For<IMediator>();
            _subMapper = Substitute.For<IMapper>();
        }

        private AppointmentService CreateService()
        {
            return new AppointmentService(
                _subMediator,
                _subMapper);
        }

        [Test]
        public async Task AddProjectAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            AppointmentService service = CreateService();
            var addProjectDto = new AppointmentAddProjectDto();
            var command = new Appointments.AddProject.Command(Guid.NewGuid(), Guid.NewGuid());
            _subMapper.Map<Appointments.AddProject.Command>(addProjectDto)
                .Returns(command);

            // Act
            await service.AddProjectAsync(
                addProjectDto);

            // Assert
            await _subMediator.Received().Send(command);
        }

        [Test]
        public async Task AddSectionAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            AppointmentService service = CreateService();
            var addSectionDto = new AppointmentAddSectionDto();
            var command = new Appointments.AddSection.Command(Guid.NewGuid(), Guid.NewGuid());
            _subMapper.Map<Appointments.AddSection.Command>(addSectionDto)
                .Returns(command);

            // Act
            await service.AddSectionAsync(
                addSectionDto);

            // Assert
            await _subMediator.Received().Send(command);
        }

        [Test]
        public async Task AddRoomAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            AppointmentService service = CreateService();
            var addRoomDto = new AppointmentAddRoomDto();
            var command = new Appointments.AddRoom.Command(Guid.NewGuid(), Guid.NewGuid());
            _subMapper.Map<Appointments.AddRoom.Command>(addRoomDto)
                .Returns(command);

            // Act
            await service.AddRoomAsync(
                addRoomDto);

            // Assert
            await _subMediator.Received().Send(command);
        }

        [Test]
        public async Task CreateAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            AppointmentService service = CreateService();
            AppointmentCreateDto createDto = null;
            AppointmentDto expectedDto = AppointmentDtoData.RockingXMasRehearsal;
            _subMapper.Map<AppointmentDto>(Arg.Any<Appointment>()).Returns(expectedDto);

            // Act
            AppointmentDto result = await service.CreateAsync(
                createDto);

            // Assert
            result.Should().Be(expectedDto);
        }

        [Test]
        public async Task GetAsync_By_Id_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            AppointmentService service = CreateService();
            Guid id = Guid.Empty;
            AppointmentDto expectedDto = AppointmentDtoData.RockingXMasRehearsal;
            _subMapper.Map<AppointmentDto>(Arg.Any<Appointment>())
                .Returns(expectedDto);
            _subMediator.Send(Arg.Any<Domain.GenericHandlers.Details.Query<Appointment>>())
                .Returns(AppointmentSeedData.RockingXMasRehearsal);

            // Act
            AppointmentDto result = await service.GetByIdAsync(id);

            // Assert
            result.Should().BeEquivalentTo(expectedDto);
        }

        [Test]
        public async Task ModifyAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            AppointmentService service = CreateService();
            AppointmentModifyDto appointmentModifyDto = null;
            var command = new Appointments.Modify.Command();
            _subMapper.Map<Appointments.Modify.Command>(appointmentModifyDto)
                .Returns(command);

            // Act
            await service.ModifyAsync(
                appointmentModifyDto);

            // Assert
            await _subMediator.Received().Send(command);
        }

        [Test]
        public async Task RemoveProjectAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            AppointmentService service = CreateService();
            AppointmentRemoveProjectDto removeProjectDto = null;
            var command = new Appointments.RemoveProject.Command(Guid.Empty, Guid.Empty);
            _subMapper.Map<Appointments.RemoveProject.Command>(removeProjectDto)
                .Returns(command);

            // Act
            await service.RemoveProjectAsync(
                removeProjectDto);

            // Assert
            await _subMediator.Received().Send(command);
        }

        [Test]
        public async Task RemoveSectionAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            AppointmentService service = CreateService();
            AppointmentRemoveSectionDto removeSectionDto = null;
            var command = new Appointments.RemoveSection.Command(Guid.Empty, Guid.Empty);
            _subMapper.Map<Appointments.RemoveSection.Command>(removeSectionDto)
                .Returns(command);

            // Act
            await service.RemoveSectionAsync(
                removeSectionDto);

            // Assert
            await _subMediator.Received().Send(command);
        }

        [Test]
        public async Task RemoveRoomAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            AppointmentService service = CreateService();
            AppointmentRemoveRoomDto removeRoomDto = null;
            var command = new Appointments.RemoveRoom.Command(Guid.Empty, Guid.Empty);
            _subMapper.Map<Appointments.RemoveRoom.Command>(removeRoomDto)
                .Returns(command);

            // Act
            await service.RemoveRoomAsync(
                removeRoomDto);

            // Assert
            await _subMediator.Received().Send(command);
        }

        [Test]
        public async Task SetDatesAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            AppointmentService service = CreateService();
            AppointmentSetDatesDto setDatesDto = null;
            var command = new Appointments.SetDates.Command();
            _subMapper.Map<Appointments.SetDates.Command>(setDatesDto)
                .Returns(command);

            // Act
            await service.SetDatesAsync(
                setDatesDto);

            // Assert
            await _subMediator.Received().Send(command);
        }

        [Test]
        public async Task SetVenueAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            AppointmentService service = CreateService();
            AppointmentSetVenueDto setVenueDto = null;
            var command = new Appointments.SetVenue.Command(Guid.Empty, Guid.Empty);
            _subMapper.Map<Appointments.SetVenue.Command>(setVenueDto)
                .Returns(command);

            // Act
            await service.SetVenueAsync(
                setVenueDto);

            // Assert
            await _subMediator.Received().Send(command);
        }

        [Test]
        public async Task SetParticipationResultAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            AppointmentService service = CreateService();
            AppointmentParticipationSetResultDto dto = null;
            var command = new AppointmentParticipations.SetResult.Command();
            _subMapper.Map<AppointmentParticipations.SetResult.Command>(dto)
                .Returns(command);

            // Act
            await service.SetParticipationResultAsync(
                dto);

            // Assert
            await _subMediator.Received().Send(command);
        }

        [Test]
        public async Task DeleteAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            AppointmentService service = CreateService();
            var id = default(Guid);

            // Act
            await service.DeleteAsync(id);

            // Assert
            await _subMediator.Received().Send(Arg.Any<Domain.GenericHandlers.Delete.Command<Appointment>>());
        }
    }
}
