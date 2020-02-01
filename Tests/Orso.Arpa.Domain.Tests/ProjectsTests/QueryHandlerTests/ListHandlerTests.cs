using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MockQueryable.NSubstitute;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Domain.Tests.ProjectsTests.QueryHandlerTests
{
    [TestFixture]
    public class ListHandlerTests
    {
        private IReadOnlyRepository _repository;
        private Logic.Projects.List.Handler _handler;

        [SetUp]
        public void Setup()
        {
            _repository = Substitute.For<IReadOnlyRepository>();
            _handler = new Logic.Projects.List.Handler(_repository);
        }

        [Test]
        public async Task Should_Get_List()
        {
            // Arrange
            var expectedProjects = ProjectSeedData.Projects.ToImmutableList();
            IQueryable<Project> projectsToReturn = ProjectSeedData.Projects.AsQueryable().BuildMock();
            _repository.GetAll<Project>()
                .Returns(projectsToReturn);

            // Act
            IImmutableList<Project> result = await _handler.Handle(
                new Logic.Projects.List.Query(),
                new CancellationToken());

            // Assert
            result.Should().BeEquivalentTo(expectedProjects);
        }
    }
}
