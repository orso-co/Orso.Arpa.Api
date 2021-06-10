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
    public class GetForProjectQueryHandlerTests
    {
        private IArpaContext _arpaContext;
        private GetForProject.Handler _handler;

        [SetUp]
        public void Setup()
        {
            _arpaContext = Substitute.For<IArpaContext>();
            DbSet<ProjectParticipation> projectParticipations = MockDbSets.ProjectParticipations;
            _arpaContext.ProjectParticipations.Returns(projectParticipations);
            _handler = new GetForProject.Handler(_arpaContext);
        }

        [Test]
        public async Task Should_Get_ProjectParticipations()
        {
            // Arrange
            var query = new GetForProject.Query
            {
                ProjectId = ProjectSeedData.RockingXMas.Id
            };
            var expectedIds = new Guid[]
            {
                ProjectParticipationSeedData.AdminParticipation.Id,
                ProjectParticipationSeedData.PerformerRockingXMasParticipation.Id,
                ProjectParticipationSeedData.StaffParticipation1.Id,
                ProjectParticipationSeedData.StaffParticipation2.Id
            };

            // Act
            IEnumerable<ProjectParticipation> result = await _handler.Handle(query, new CancellationToken());

            // Assert
            result.Select(r => r.Id).Should().BeEquivalentTo(expectedIds, opt => opt.WithStrictOrdering());
        }
    }
}
