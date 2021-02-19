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
            _handler = new Logic.Sections.Tree.Handler(_context);
        }

        private IArpaContext _context;
        private Logic.Sections.Tree.Handler _handler;

        [Test]
        public async Task Should_Get_Section_Tree()
        {
            // Arrange
            var treeQuery = new Logic.Sections.Tree.Query { MaxLevel = 2 };
            DbSet<Section> mockSections = MockDbSets.Sections;
            _context.Sections.Returns(mockSections);

            // Act
            ITree<Section> tree = await _handler.Handle(treeQuery, new CancellationToken());

            // Assert
            tree.Children.Count.Should().Be(5);
            tree.Data.Should().BeNull();
            tree.Level.Should().Be(0);
            tree.IsRoot.Should().BeTrue();
            tree.IsLeaf.Should().BeFalse();
            tree.Children.Select(c => c.Data.Name).Should().Equal("Other", "Performers", "Volunteers", "Visitors", "Crew");
            tree.Children.Skip(1).First().Children.Count.Should().Be(4);
            tree.Children.Skip(1).First().Children.Select(c => c.Data.Name).Should().Equal("Choir", "Orchestra", "Soloists", "Band");
            tree.Children.Last().Children.Count.Should().Be(4);
            tree.Children.Skip(1).First().Children.First().Children.Count.Should().Be(0);
        }
    }
}
