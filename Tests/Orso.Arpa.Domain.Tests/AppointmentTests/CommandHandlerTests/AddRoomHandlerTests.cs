using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Domain.Logic.Appointments;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Domain.Tests.AppointmentTests.CommandHandlerTests
{
    [TestFixture]
    public class AddRoomHandlerTests
    {
        private IArpaContext _arpaContext;
        private AddRoom.Handler _handler;

        [SetUp]
        public void Setup()
        {
            _arpaContext = Substitute.For<IArpaContext>();
            _handler = new AddRoom.Handler(_arpaContext);
        }

        [Test]
        public async Task Should_Add_Room()
        {
            // Arrange
            DbSet<Appointment> mockData = MockDbSets.Appointments;
            Appointment appointment = AppointmentSeedData.AfterShowParty;
            mockData.FindAsync(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(appointment);
            _arpaContext.Appointments.Returns(mockData);
            _arpaContext.SaveChangesAsync(Arg.Any<CancellationToken>()).Returns(1);

            // Act
            Unit result = await _handler.Handle(new AddRoom.Command(
                appointment.Id,
                RoomSeedData.MusikraumWeiherhofSchule.Id), new CancellationToken());

            // Assert
            result.Should().BeEquivalentTo(Unit.Value);
        }
    }
}
