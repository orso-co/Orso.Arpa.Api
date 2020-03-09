using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Domain.Tests.GenericHandlerTests
{
    [TestFixture]
    public class ListHandlerTests
    {
        private IReadOnlyRepository _repository;
        private GenericHandlers.List.Handler<Appointment> _handler;

        [SetUp]
        public void Setup()
        {
            _repository = Substitute.For<IReadOnlyRepository>();
            _handler = new GenericHandlers.List.Handler<Appointment>(_repository);
        }

        [Test]
        public async Task Should_Get_List()
        {
            // Arrange
            var expectedAppointments = AppointmentSeedData.Appointments.ToImmutableList();
            Appointment appointment = AppointmentSeedData.RockingXMasConcert;
            _repository.GetAll<Appointment>()
                .Returns(AppointmentSeedData.Appointments.AsQueryable());

            // Act
            IQueryable<Appointment> result = await _handler.Handle(
                new GenericHandlers.List.Query<Appointment>(a => a.StartTime >= DateTime.MinValue && a.StartTime <= DateTime.MaxValue),
                new CancellationToken());

            // Assert
            result.Should().BeEquivalentTo(expectedAppointments);
        }
    }
}
