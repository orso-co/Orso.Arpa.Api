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
    public class TreeHandlerTests
    {
        [SetUp]
        public void Setup()
        {
            _context = Substitute.For<IArpaContext>();
            _handler = new Domain.Logic.Sections.Tree.Handler(_context);
        }

        private IArpaContext _context;
        private Domain.Logic.Sections.Tree.Handler _handler;

        [Test]
        public async Task Should_Get_Section_Tree()
        {
            // Arrange
            var treeQuery = new Domain.Logic.Sections.Tree.Query { MaxLevel = 3 };
            DbSet<Section> mockSections = MockDbSets.Sections;
            _ = _context.Sections.Returns(mockSections);

            // Act
            ITree<Section> tree = await _handler.Handle(treeQuery, new CancellationToken());

            // Assert
            // Assert root element
            _ = tree.Children.Count.Should().Be(6);         // should have this amount of children
            _ = tree.Data.Should().BeNull();                // data should be null
            _ = tree.Level.Should().Be(0);                  // level is top level
            _ = tree.IsRoot.Should().BeTrue();              // this is the only root element in the whole tree
            _ = tree.IsLeaf.Should().BeFalse();             // the root is not a leaf
                                                            // these are the expected values of the roots children:
            _ = tree.Children.Select(c => c.Data.Name).Should().Equal("Performers", "Members", "Visitors", "Volunteers", "Suppliers", "Contractors");

            // Assert a specific sub-tree
            ITree<Section> subtree = tree.Children.First();            // navigate to first element in root tree, which is "Performers"
            subtree = subtree.Children.Skip(1).First(); // skip "Conductor", which makes "Choir" the first in the sub list now
            _ = subtree.Children.Count.Should().Be(2);      // the "Choir" has two sub nodes called "Female Voices" and "Male Voices"
            _ = subtree.Children.Select(c => c.Data.Name).Should().Equal("Female Voices", "Male Voices");

            // Assert a last node in root tree, which is "Contractors"
            subtree = tree.Children.Last();             // get the "Contractors" node
            _ = subtree.Children.Count.Should().Be(0);      // currently no sub nodes defined
        }
    }
}
