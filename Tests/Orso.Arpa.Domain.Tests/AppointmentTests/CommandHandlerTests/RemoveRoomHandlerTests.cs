using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
    public class RemoveRoomHandlerTests
    {
        private IArpaContext _arpaContext;
        private RemoveRoom.Handler _handler;

        [SetUp]
        public void Setup()
        {
            _arpaContext = Substitute.For<IArpaContext>();
            _handler = new RemoveRoom.Handler(_arpaContext);
        }

        [Test]
        public async Task Should_Remove_Project()
        {
            // Arrange
            DbSet<AppointmentRoom> mockData = MockDbSets.AppointmentRooms;
            AppointmentRoom appointmentRoom = AppointmentSeedData.AfterShowParty.AppointmentRooms.First();
            _arpaContext.AppointmentRooms.Returns(mockData);
            _arpaContext.SaveChangesAsync(Arg.Any<CancellationToken>()).Returns(1);

            // Act
            Unit result = await _handler.Handle(new RemoveRoom.Command(
                appointmentRoom.AppointmentId,
               appointmentRoom.RoomId), new CancellationToken());

            // Assert
            result.Should().BeEquivalentTo(Unit.Value);
        }
    }
}
