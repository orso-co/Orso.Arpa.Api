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
using Orso.Arpa.Domain.Logic.MusicianProfiles;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Domain.Tests.MeTests.QueryHandlerTests
{
    [TestFixture]
    public class GetForAppointmentHandlerTests
    {
        private IArpaContext _arpaContext;
        private GetForAppointment.Handler _handler;

        [SetUp]
        public void Setup()
        {
            _arpaContext = Substitute.For<IArpaContext>();
            _handler = new GetForAppointment.Handler(_arpaContext);
        }

        [Test]
        public async Task Should_Get_Person_Grouping()
        {
            // Arrange
            DbSet<MusicianProfile> mockData = MockDbSets.MusicianProfiles;
            mockData.AsAsyncEnumerable().Returns(GetTestValues());
            _arpaContext.MusicianProfiles.Returns(mockData);
            Person person = FakePersons.Performer;
            var expectedPersonGrouping = new GetForAppointment.PersonGrouping
            {
                Person = PersonTestSeedData.Performer,
                Participation = null,
                Profiles = new List<MusicianProfile>
                {
                    FakeMusicianProfiles.PerformerMusicianProfile
                }
            };

            // Act
            IEnumerable<GetForAppointment.PersonGrouping> result = await _handler.Handle(
                new GetForAppointment.Query
                {
                    Appointment = FakeAppointments.RockingXMasRehearsal,
                    SectionTree = new List<ITree<Section>>()
                },
                new CancellationToken());

            // Assert
            result.Count().Should().Be(1);
            GetForAppointment.PersonGrouping group = result.First();
            group.Should().BeEquivalentTo(expectedPersonGrouping, opt => opt.Excluding(group => group.Profiles));
            group.Profiles.Count().Should().Be(expectedPersonGrouping.Profiles.Count());
            group.Profiles.First().Should().BeEquivalentTo(expectedPersonGrouping.Profiles.First(), opt => opt.Excluding(profile => profile.ProjectParticipations));
        }

        private static async IAsyncEnumerable<MusicianProfile> GetTestValues()
        {
            yield return FakeMusicianProfiles.PerformerMusicianProfile;
            await Task.CompletedTask;
        }
    }
}
