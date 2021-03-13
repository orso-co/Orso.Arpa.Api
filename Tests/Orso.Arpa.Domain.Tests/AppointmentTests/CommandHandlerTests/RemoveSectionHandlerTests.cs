using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
    public class RemoveSectionHandlerTests
    {
        private IArpaContext _arpaContext;
        private RemoveSection.Handler _handler;

        [SetUp]
        public void Setup()
        {
            _arpaContext = Substitute.For<IArpaContext>();
            _handler = new RemoveSection.Handler(_arpaContext);
        }

        [Test]
        public async Task Should_Remove_Section()
        {
            // Arrange
            DbSet<SectionAppointment> mockData = MockDbSets.SectionAppointments;
            SectionAppointment sectionAppointment = AppointmentSeedData.AfterShowParty.SectionAppointments.First();
            _arpaContext.SectionAppointments.Returns(mockData);
            _arpaContext.SaveChangesAsync(Arg.Any<CancellationToken>()).Returns(1);

            // Act
            Unit result = await _handler.Handle(new RemoveSection.Command(
                sectionAppointment.AppointmentId,
                sectionAppointment.SectionId), new CancellationToken());

            // Assert
            result.Should().BeEquivalentTo(Unit.Value);
        }
    }
}
