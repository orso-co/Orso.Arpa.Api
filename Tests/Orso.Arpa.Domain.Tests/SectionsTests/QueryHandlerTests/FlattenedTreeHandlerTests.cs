using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.SectionDomain.Model;
using Orso.Arpa.Domain.SectionDomain.Queries;
using Orso.Arpa.Tests.Shared.FakeData;

namespace Orso.Arpa.Domain.Tests.RolesTests.QueryHandlerTests
{
    public class FlattenedTreeHandlerTests
    {
        [SetUp]
        public void Setup()
        {
            _context = Substitute.For<IArpaContext>();
            _handler = new ListFlattenedSectionTree.Handler(_context);
        }

        private IArpaContext _context;
        private ListFlattenedSectionTree.Handler _handler;

        [Test]
        public async Task Should_Get_Flattened_Section_Tree()
        {
            // Arrange
            var treeQuery = new ListFlattenedSectionTree.Query { MaxLevel = 3 };
            DbSet<Section> mockSections = MockDbSets.Sections;
            _ = _context.Sections.Returns(mockSections);

            // Act
            IEnumerable<ITree<Section>> tree = await _handler.Handle(treeQuery, new CancellationToken());

            // Assert
            _ = tree.Count().Should().Be(33);
            _ = tree.Select(t => t.Level).Distinct().Count().Should().Be(3);
            _ = tree.First().Data.Name.Should().Be("Assistant Conductor");
            _ = tree.Last().Data.Name.Should().Be("Contractors");
        }
    }
}
