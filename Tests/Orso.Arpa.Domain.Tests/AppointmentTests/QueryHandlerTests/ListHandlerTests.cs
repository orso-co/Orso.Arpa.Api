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

namespace Orso.Arpa.Domain.Tests.AppointmentTests.QueryHandlerTests
{
    [TestFixture]
    public class ListHandlerTests
    {
        private IReadOnlyRepository _repository;
        private Logic.Appointments.List.Handler _handler;

        [SetUp]
        public void Setup()
        {
            _repository = Substitute.For<IReadOnlyRepository>();
            _handler = new Logic.Appointments.List.Handler(_repository);
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
            IImmutableList<Appointment> result = await _handler.Handle(
                new Logic.Appointments.List.Query
                {
                    StartTime = DateTime.MinValue,
                    EndTime = DateTime.MaxValue
                },
                new CancellationToken());

            // Assert
            result.Should().BeEquivalentTo(expectedAppointments);
        }
    }
}
