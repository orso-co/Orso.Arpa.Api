using System;
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
    public class SetDatesHandlerTests
    {
        private IArpaContext _arpaContext;
        private IMapper _mapper;
        private SetDates.Handler _handler;

        [SetUp]
        public void Setup()
        {
            _arpaContext = Substitute.For<IArpaContext>();
            _mapper = Substitute.For<IMapper>();
            _handler = new SetDates.Handler(_arpaContext, _mapper);
        }

        [Test]
        public async Task Should_Set_Dates()
        {
            // Arrange
            DbSet<Appointment> mockData = MockDbSets.Appointments;
            Appointment appointment = AppointmentSeedData.RockingXMasConcert;
            mockData.FindAsync(Arg.Any<Guid>()).Returns(appointment);
            _arpaContext.Appointments.Returns(mockData);
            _arpaContext.SaveChangesAsync(Arg.Any<CancellationToken>()).Returns(1);

            // Act
            Unit result = await _handler.Handle(
                new SetDates.Command
                {
                    StartTime = DateTime.UtcNow,
                    EndTime = DateTime.UtcNow.AddHours(2),
                    Id = appointment.Id
                },
                new CancellationToken());

            // Assert
            result.Should().BeEquivalentTo(Unit.Value);
        }
    }
}
