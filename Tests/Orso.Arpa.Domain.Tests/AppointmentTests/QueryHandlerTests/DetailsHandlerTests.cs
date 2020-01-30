using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Domain.Logic.Appointments;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Domain.Tests.AppointmentTests.QueryHandlerTests
{
    [TestFixture]
    public class DetailsHandlerTests
    {
        private IReadOnlyRepository _repository;
        private Details.Handler _handler;

        [SetUp]
        public void Setup()
        {
            _repository = Substitute.For<IReadOnlyRepository>();
            _handler = new Details.Handler(_repository);
        }

        [Test]
        public async Task Should_Get_Details()
        {
            // Arrange
            Appointment appointment = AppointmentSeedData.RockingXMasConcert;
            _repository.GetByIdAsync<Appointment>(Arg.Any<Guid>())
                .Returns(appointment);

            // Act
            Appointment result = await _handler.Handle(new Details.Query(), new CancellationToken());

            // Assert
            result.Should().BeEquivalentTo(appointment);
        }
    }
}
