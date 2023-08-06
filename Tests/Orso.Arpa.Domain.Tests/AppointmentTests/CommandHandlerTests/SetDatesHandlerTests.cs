using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Domain.Logic.Appointments;
using Orso.Arpa.Tests.Shared.Extensions;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Domain.Tests.AppointmentTests.CommandHandlerTests
{
    [TestFixture]
    public class SetDatesHandlerTests
    {
        private IArpaContext _arpaContext;
        private SetDates.Handler _handler;

        [SetUp]
        public void Setup()
        {
            _arpaContext = Substitute.For<IArpaContext>();
            _handler = new SetDates.Handler(_arpaContext);
        }

        [Test]
        public async Task Should_Set_Dates()
        {
            // Arrange
            DateTime startTime = FakeDateTime.UtcNow;
            DateTime endTime = FakeDateTime.UtcNow.AddHours(2);
            Appointment expectedAppointment = FakeAppointments.RockingXMasRehearsal;
            expectedAppointment.SetProperty(nameof(Appointment.EndTime), endTime);
            expectedAppointment.SetProperty(nameof(Appointment.StartTime), startTime);
            DbSet<Appointment> mockData = MockDbSets.Appointments;
            Appointment appointment = AppointmentSeedData.RockingXMasRehearsal;
            mockData.FindAsync(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(appointment);
            // Could not find a way to mock EntityEntry<Appointment> as it does not provide a suitable constructor. If you know a way how to mock it, feel free to improve this test.
            //EntityEntry<Appointment> mockedEntityEntry = Substitute.For<EntityEntry<Appointment>>();
            //mockedEntityEntry.Entity.Returns(expectedAppointment);
            //mockData.Update(Arg.Any<Appointment>()).Returns(mockedEntityEntry);
            _arpaContext.Appointments.Returns(mockData);
            _arpaContext.SaveChangesAsync(Arg.Any<CancellationToken>()).Returns(1);

            // Act
            Appointment result = await _handler.Handle(
                new SetDates.Command
                {
                    StartTime = startTime,
                    EndTime = endTime,
                    Id = appointment.Id
                },
                new CancellationToken());

            // Assert
            result.Should().BeNull();
            // result.Should().BeEquivalentTo(expectedAppointment);
        }
    }
}
