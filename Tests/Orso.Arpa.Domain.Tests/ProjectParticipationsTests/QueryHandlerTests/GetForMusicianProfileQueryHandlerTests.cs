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
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Domain.Logic.ProjectParticipations;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Domain.Tests.ProjectParticipationsTests.QueryHandlerTests
{
    [TestFixture]
    public class GetForMusicianProfileQueryHandlerTests
    {
        private IArpaContext _arpaContext;
        private GetForMusicianProfile.Handler _handler;

        [SetUp]
        public void Setup()
        {
            _arpaContext = Substitute.For<IArpaContext>();
            DbSet<ProjectParticipation> projectParticipations = MockDbSets.ProjectParticipations;
            _arpaContext.ProjectParticipations.Returns(projectParticipations);
            _handler = new GetForMusicianProfile.Handler(_arpaContext);
        }

        [Test]
        public async Task Should_Get_ProjectParticipations_Excluding_Completed_Projects()
        {
            // Arrange
            var query = new GetForMusicianProfile.Query
            {
                IncludeCompletedProjects = false,
                MusicianProfileId = MusicianProfileSeedData.PerformerMusicianProfile.Id
            };

            // Act
            IEnumerable<ProjectParticipation> result = await _handler.Handle(query, new CancellationToken());

            // Assert
            result.Count().Should().Be(1);
            result.First().Id.Should().Be(ProjectParticipationSeedData.PerformerSchneeköniginParticipation.Id);
        }

        [Test]
        public async Task Should_Get_ProjectParticipations_Including_Completed_Projects()
        {
            // Arrange
            var query = new GetForMusicianProfile.Query
            {
                IncludeCompletedProjects = true,
                MusicianProfileId = MusicianProfileSeedData.PerformerMusicianProfile.Id
            };
            var expectedIds = new Guid[]
            {
                ProjectParticipationSeedData.PerformerRockingXMasParticipation.Id,
                ProjectParticipationSeedData.PerformerSchneeköniginParticipation.Id
            };

            // Act
            IEnumerable<ProjectParticipation> result = await _handler.Handle(query, new CancellationToken());

            // Assert
            result.Select(r => r.Id).Should().BeEquivalentTo(expectedIds, opt => opt.WithStrictOrdering());
        }
    }
}
