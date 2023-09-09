using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.AppointmentDomain.Model;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;
using List = Orso.Arpa.Domain.General.GenericHandlers.List;

namespace Orso.Arpa.Domain.Tests.GenericHandlerTests
{
    [TestFixture]
    public class ListHandlerTests
    {
        private IArpaContext _arpaContext;
        private List.Handler<Appointment> _handler;

        [SetUp]
        public void Setup()
        {
            _arpaContext = Substitute.For<IArpaContext>();
            _handler = new List.Handler<Appointment>(_arpaContext);
        }

        [Test]
        public async Task Should_Get_List()
        {
            // Arrange
            var expectedAppointments = AppointmentSeedData.Appointments.ToImmutableList();
            Appointment appointment = AppointmentSeedData.RockingXMasConcert;
            DbSet<Appointment> mockAppointments = MockDbSets.Appointments;
            _arpaContext.Set<Appointment>().Returns(mockAppointments);

            // Act
            IQueryable<Appointment> result = await _handler.Handle(
                new List.Query<Appointment>(a => a.StartTime >= DateTime.MinValue && a.StartTime <= DateTime.MaxValue),
                new CancellationToken());

            // Assert
            result.Should().BeEquivalentTo(expectedAppointments);
        }
    }
}
