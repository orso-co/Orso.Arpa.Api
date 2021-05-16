using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MockQueryable.NSubstitute;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Extensions;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Domain.Logic.Me;
using Orso.Arpa.Domain.Views;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;

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
            Person person = FakePersons.Performer;
            var expectedAppointments = new List<Appointment> { FakeAppointments.RockingXMasRehearsal };

            DbSet<Appointment> appointmentMockData = MockDbSets.Appointments;
            appointmentMockData.AsAsyncEnumerable().Returns(GetTestValues());
            appointmentMockData.AsQueryable().Returns(appointmentMockData);
            _arpaContext.Appointments.Returns(appointmentMockData);

            DbSet<AppointmentForPerson> idList = (new List<AppointmentForPerson>() { new AppointmentForPerson { Id = AppointmentSeedData.RockingXMasRehearsal.Id } }).AsQueryable().BuildMockDbSet();
            idList.AsAsyncEnumerable().Returns(GetIds());
            _arpaContext.GetAppointmentIdsForPerson(person.Id).Returns(idList);

            // Act
            Tuple<IQueryable<Appointment>, int> result = await _handler.Handle(
                new AppointmentList.Query(null, null, new List<ITree<Section>>(), person),
                new CancellationToken());

            // Assert
            result.Item1.Count().Should().Be(1);
            result.Item1.First().Id.Should().Be(expectedAppointments.First().Id);
            result.Item2.Should().Be(1);
        }

        private static async IAsyncEnumerable<Appointment> GetTestValues()
        {
            yield return FakeAppointments.RockingXMasRehearsal;
            await Task.CompletedTask;
        }

        private static async IAsyncEnumerable<AppointmentForPerson> GetIds()
        {
            yield return new AppointmentForPerson { Id = AppointmentSeedData.RockingXMasRehearsal.Id };
            await Task.CompletedTask;
        }
    }
}
