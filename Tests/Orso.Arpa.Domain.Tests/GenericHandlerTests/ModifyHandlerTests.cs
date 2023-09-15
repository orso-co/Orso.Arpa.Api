using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.AppointmentDomain.Commands;
using Orso.Arpa.Domain.AppointmentDomain.Model;
using Orso.Arpa.Domain.General.GenericHandlers;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Domain.Tests.GenericHandlerTests
{
    [TestFixture]
    public class ModifyHandlerTests
    {
        private IArpaContext _arpaContext;
        private Modify.Handler<Appointment> _handler;

        [SetUp]
        public void Setup()
        {
            _arpaContext = Substitute.For<IArpaContext>();
            _handler = new Modify.Handler<Appointment>(_arpaContext);
        }

        [Test]
        public async Task Should_Modify()
        {
            // Arrange
            Appointment expectedAppointment = AppointmentSeedData.RockingXMasConcert;
            _ = _arpaContext.FindAsync<Appointment>(Arg.Any<object[]>(), Arg.Any<CancellationToken>())
                .Returns(expectedAppointment);
            _ = _arpaContext.SaveChangesAsync(Arg.Any<CancellationToken>())
                .Returns(1);

            // Act
            Unit result = await _handler.Handle(new ModifyAppointment.Command(), new CancellationToken());

            // Assert
            _ = result.Should().BeEquivalentTo(Unit.Value);
        }
    }
}
