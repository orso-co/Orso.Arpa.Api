using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.AppointmentDomain.Commands;
using Orso.Arpa.Domain.AppointmentDomain.Model;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Domain.Tests.AppointmentTests.CommandHandlerTests
{
    [TestFixture]
    public class SetVenueHandlerTests
    {
        private IArpaContext _arpaContext;
        private SetVenue.Handler _handler;

        [SetUp]
        public void Setup()
        {
            _arpaContext = Substitute.For<IArpaContext>();
            _handler = new SetVenue.Handler(_arpaContext);
        }

        [Test]
        public async Task Should_Set_Dates()
        {
            // Arrange
            DbSet<Appointment> mockData = MockDbSets.Appointments;
            Appointment appointment = AppointmentSeedData.AppointmentWithoutProject;
            mockData.FindAsync(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(appointment);
            _arpaContext.Set<Appointment>().Returns(mockData);
            _arpaContext.SaveChangesAsync(Arg.Any<CancellationToken>()).Returns(1);

            // Act
            Unit result = await _handler.Handle(
                new SetVenue.Command(appointment.Id, Guid.NewGuid()),
                new CancellationToken());

            // Assert
            result.Should().BeEquivalentTo(Unit.Value);
        }
    }
}
