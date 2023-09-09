using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.AppointmentDomain.Model;
using Orso.Arpa.Domain.General.GenericHandlers;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Domain.Tests.GenericHandlerTests
{
    [TestFixture]
    public class DetailsHandlerTests
    {
        private IArpaContext _arpaContext;
        private Details.Handler<Appointment> _handler;

        [SetUp]
        public void Setup()
        {
            _arpaContext = Substitute.For<IArpaContext>();
            _handler = new Details.Handler<Appointment>(_arpaContext);
        }

        [Test]
        public async Task Should_Get_Details()
        {
            // Arrange
            Appointment appointment = AppointmentSeedData.RockingXMasConcert;
            _arpaContext.FindAsync<Appointment>(Arg.Any<object[]>(), Arg.Any<CancellationToken>())
                .Returns(appointment);

            // Act
            Appointment result = await _handler
                .Handle(new Details.Query<Appointment>(Guid.NewGuid()), new CancellationToken());

            // Assert
            result.Should().BeEquivalentTo(appointment);
        }
    }
}
