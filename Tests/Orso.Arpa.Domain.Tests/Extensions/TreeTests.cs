using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Extensions;
using Orso.Arpa.Persistence.Seed;

namespace Orso.Arpa.Domain.Tests.Extensions
{
    [TestFixture]
    public class TreeTests
    {
        private IList<Section> _sections;

        [SetUp]
        public void SetUp()
        {
            _sections = SectionSeedData.Sections;
        }

        [Test]
        public void Should_Create_Tree()
        {
            // Arrange

            // Act
            ITree<Section> tree = _sections.ToTree((parent, child) => child.ParentId == parent.Id, 2);

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

        [Test]
        public void Should_Flatten()
        {
            // Arrange
            ITree<Section> virtualRootNode = _sections.ToTree((parent, child) => child.ParentId == parent.Id);

            // Act
            IEnumerable<ITree<Section>> flattenedTree = virtualRootNode.Children.Flatten(node => node.Children);

            // Assert
            flattenedTree.Count().Should().Be(_sections.Count);
            flattenedTree.Select(t => t.Data.Name).Should().BeEquivalentTo(_sections.Select(s => s.Name));
        }

        [Test]
        public void Should_Get_Parent_Nodes()
        {
            // Arrange
            ITree<Section> virtualRootNode = _sections.ToTree((parent, child) => child.ParentId == parent.Id);
            IEnumerable<ITree<Section>> flattenedTree = virtualRootNode.Children.Flatten(node => node.Children);
            ITree<Section> node = flattenedTree.First(node => node.Data.Name == "Alto 2");

            // Act
            List<Section> parents = node.GetParents();

            // Assert
            parents.Select(p => p.Name).Should().Equal("Alto", "Deep Female Voices", "Female Voices", "Choir", "Performers");
        }
    }
}
