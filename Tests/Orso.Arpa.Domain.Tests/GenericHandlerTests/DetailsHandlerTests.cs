using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.GenericHandlers;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Domain.Tests.GenericHandlerTests
{
    [TestFixture]
    public class DetailsHandlerTests
    {
        private IReadOnlyRepository _repository;
        private Details.Handler<Appointment> _handler;

        [SetUp]
        public void Setup()
        {
            _repository = Substitute.For<IReadOnlyRepository>();
            _handler = new Details.Handler<Appointment>(_repository);
        }

        [Test]
        public async Task Should_Get_Details()
        {
            // Arrange
            Appointment appointment = AppointmentSeedData.RockingXMasConcert;
            _repository.GetByIdAsync<Appointment>(Arg.Any<Guid>())
                .Returns(appointment);

            // Act
            Appointment result = await _handler.Handle(new Details.Query<Appointment>(Guid.NewGuid()), new CancellationToken());

            // Assert
            result.Should().BeEquivalentTo(appointment);
        }
    }
}
