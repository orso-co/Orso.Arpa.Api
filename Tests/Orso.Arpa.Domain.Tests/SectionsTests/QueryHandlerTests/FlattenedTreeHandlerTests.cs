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
using Orso.Arpa.Tests.Shared.FakeData;

namespace Orso.Arpa.Domain.Tests.RolesTests.QueryHandlerTests
{
    public class FlattenedTreeHandlerTests
    {
        [SetUp]
        public void Setup()
        {
            _context = Substitute.For<IArpaContext>();
            _handler = new Logic.Sections.FlattenedTree.Handler(_context);
        }

        private IArpaContext _context;
        private Logic.Sections.FlattenedTree.Handler _handler;

        [Test]
        public async Task Should_Get_Flattened_Section_Tree()
        {
            // Arrange
            var treeQuery = new Logic.Sections.FlattenedTree.Query { MaxLevel = 3 };
            DbSet<Section> mockSections = MockDbSets.Sections;
            _context.Sections.Returns(mockSections);

            // Act
            IEnumerable<ITree<Section>> tree = await _handler.Handle(treeQuery, new CancellationToken());

            // Assert
            tree.Count().Should().Be(24);
            tree.Select(t => t.Level).Distinct().Count().Should().Be(3);
            tree.First().Data.Name.Should().Be("Assistant Conductor");
            tree.Last().Data.Name.Should().Be("Contractors");
        }
    }
}
