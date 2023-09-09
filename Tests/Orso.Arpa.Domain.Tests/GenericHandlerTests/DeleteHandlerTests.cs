using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.AppointmentDomain.Model;
using Orso.Arpa.Domain.General.GenericHandlers;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Domain.Tests.GenericHandlerTests
{
    [TestFixture]
    public class DeleteHandlerTests
    {
        private IArpaContext _arpaContext;
        private Delete.Handler<Appointment> _handler;

        [SetUp]
        public void Setup()
        {
            _arpaContext = Substitute.For<IArpaContext>();
            _handler = new Delete.Handler<Appointment>(_arpaContext);
        }

        [Test]
        public async Task Should_Delete()
        {
            // Arrange
            _arpaContext.FindAsync<Appointment>(Arg.Any<object[]>(), Arg.Any<CancellationToken>())
                .Returns(AppointmentSeedData.AfterShowParty);
            _arpaContext.SaveChangesAsync(Arg.Any<CancellationToken>()).Returns(1);

            // Act
            Unit result = await _handler.Handle(new Delete.Command<Appointment>(), new CancellationToken());

            // Assert
            result.Should().BeEquivalentTo(Unit.Value);
        }
    }
}
