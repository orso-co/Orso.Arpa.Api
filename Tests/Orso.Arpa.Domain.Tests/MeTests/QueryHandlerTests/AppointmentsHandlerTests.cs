using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Domain.Logic.Me;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Domain.Tests.MeTests.QueryHandlerTests
{
    [TestFixture]
    public class AppointmentsHandlerTests
    {
        private IUserAccessor _userAccessor;
        private Appointments.Handler _handler;

        [SetUp]
        public void Setup()
        {
            _userAccessor = Substitute.For<IUserAccessor>();
            _handler = new Appointments.Handler(_userAccessor);
        }

        [Test]
        public async Task Should_Get_User_Appointments()
        {
            // Arrange
            User user = FakeUsers.Orsianer;
            _userAccessor.GetCurrentUserAsync().Returns(user);
            var expectedAppointments = new List<Appointment> { AppointmentSeedData.RockingXMasRehearsal };

            // Act
            IEnumerable<Appointment> result = await _handler.Handle(new Appointments.Query(null, null), new CancellationToken());

            // Assert
            result.Should().BeEquivalentTo(expectedAppointments);
        }
    }
}
