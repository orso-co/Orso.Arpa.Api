using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using MediatR;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Application.Services;
using Orso.Arpa.Domain.Appointments;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Tests.Shared.DtoTestData;
using Orso.Arpa.Tests.Shared.TestSeedData;

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
            var addProjectDto = new AddProjectDto();
            var command = new AddProject.Command(Guid.NewGuid(), Guid.NewGuid());
            _subMapper.Map<AddProject.Command>(addProjectDto)
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
            var addSectionDto = new AddSectionDto();
            var command = new AddSection.Command(Guid.NewGuid(), Guid.NewGuid());
            _subMapper.Map<AddSection.Command>(addSectionDto)
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
            var addRoomDto = new AddRoomDto();
            var command = new AddRoom.Command(Guid.NewGuid(), Guid.NewGuid());
            _subMapper.Map<AddRoom.Command>(addRoomDto)
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
        public async Task GetAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            AppointmentService service = CreateService();
            _subMediator.Send(Arg.Any<Domain.Appointments.List.Query>())
                .Returns(AppointmentSeedData.Appointments.ToImmutableList());
            IList<AppointmentDto> expectedDtos = AppointmentDtoData.Appointments;
            _subMapper.Map<IEnumerable<AppointmentDto>>(Arg.Any<IEnumerable<Appointment>>())
                .Returns(expectedDtos);

            // Act
            IEnumerable<AppointmentDto> result = await service.GetAsync(
                new DateTime(2019, 12, 22),
                DateRange.Month);

            // Assert
            result.Should().BeEquivalentTo(expectedDtos);
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

            // Act
            AppointmentDto result = await service.GetAsync(
                id);

            // Assert
            result.Should().BeEquivalentTo(expectedDto);
        }

        [Test]
        public async Task ModifyAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            AppointmentService service = CreateService();
            AppointmentModifyDto appointmentModifyDto = null;
            var command = new Modify.Command();
            _subMapper.Map<Modify.Command>(appointmentModifyDto)
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
            RemoveProjectDto removeProjectDto = null;
            var command = new RemoveProject.Command(Guid.Empty, Guid.Empty);
            _subMapper.Map<RemoveProject.Command>(removeProjectDto)
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
            RemoveSectionDto removeSectionDto = null;
            var command = new RemoveSection.Command(Guid.Empty, Guid.Empty);
            _subMapper.Map<RemoveSection.Command>(removeSectionDto)
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
            RemoveRoomDto removeRoomDto = null;
            var command = new RemoveRoom.Command(Guid.Empty, Guid.Empty);
            _subMapper.Map<RemoveRoom.Command>(removeRoomDto)
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
            SetDatesDto setDatesDto = null;
            var command = new SetDates.Command();
            _subMapper.Map<SetDates.Command>(setDatesDto)
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
            SetVenueDto setVenueDto = null;
            var command = new SetVenue.Command(Guid.Empty, Guid.Empty);
            _subMapper.Map<SetVenue.Command>(setVenueDto)
                .Returns(command);

            // Act
            await service.SetVenueAsync(
                setVenueDto);

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
            await _subMediator.Received().Send(Arg.Any<Delete.Command>());
        }
    }
}
