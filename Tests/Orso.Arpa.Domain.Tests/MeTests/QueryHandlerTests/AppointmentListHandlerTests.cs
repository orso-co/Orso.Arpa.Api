using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Extensions;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Domain.Logic.Me;
using Orso.Arpa.Misc;
using Orso.Arpa.Tests.Shared.FakeData;

namespace Orso.Arpa.Domain.Tests.MeTests.QueryHandlerTests
{
    [TestFixture]
    public class AppointmentListHandlerTests
    {
        private IArpaContext _arpaContext;
        private AppointmentList.Handler _handler;

        [SetUp]
        public void Setup()
        {
            _arpaContext = Substitute.For<IArpaContext>();
            _handler = new AppointmentList.Handler(_arpaContext);
        }

        [Test]
        public async Task Should_Get_User_Appointments()
        {
            // Arrange
            using var context = new DateTimeProviderContext(new DateTime(2021, 1, 1));
            DbSet<Appointment> mockData = MockDbSets.Appointments;
            mockData.AsAsyncEnumerable().Returns(GetTestValues());
            _arpaContext.Appointments.Returns(mockData);
            Person person = FakePersons.Performer;
            var expectedAppointments = new List<Appointment> { FakeAppointments.RockingXMasRehearsal };

            // Act
            Tuple<IEnumerable<Appointment>, int> result = await _handler.Handle(
                new AppointmentList.Query(null, null, new List<ITree<Section>>(), person),
                new CancellationToken());

            // Assert
            result.Item1.Count().Should().Be(1);
            result.Item1.First().Should().BeEquivalentTo(expectedAppointments.First(), opt=> opt.Excluding(app => app.ProjectAppointments));
            result.Item2.Should().Be(1);
        }

        private static async IAsyncEnumerable<Appointment> GetTestValues()
        {
            yield return FakeAppointments.RockingXMasRehearsal;
            await Task.CompletedTask;
        }
    }
}
