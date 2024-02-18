using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.AppointmentDomain.Commands;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.ProjectDomain.Model;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Domain.Tests.AppointmentTests.CommandHandlerTests
{
    [TestFixture]
    public class RemoveProjectHandlerTests
    {
        private IArpaContext _arpaContext;
        private RemoveProjectFromAppointment.Handler _handler;

        [SetUp]
        public void Setup()
        {
            _arpaContext = Substitute.For<IArpaContext>();
            _handler = new RemoveProjectFromAppointment.Handler(_arpaContext);
        }

        [Test]
        public async Task Should_Remove_Project()
        {
            // Arrange
            DbSet<ProjectAppointment> mockData = MockDbSets.ProjectAppointments;
            ProjectAppointment projectAppointment = AppointmentSeedData.RockingXMasRehearsal.ProjectAppointments.First();
            _arpaContext.Set<ProjectAppointment>().Returns(mockData);
            _arpaContext.SaveChangesAsync(Arg.Any<CancellationToken>()).Returns(1);

            // Act
            Unit result = await _handler.Handle(new RemoveProjectFromAppointment.Command(
                projectAppointment.AppointmentId,
                projectAppointment.ProjectId), new CancellationToken());

            // Assert
            result.Should().BeEquivalentTo(Unit.Value);
        }
    }
}
