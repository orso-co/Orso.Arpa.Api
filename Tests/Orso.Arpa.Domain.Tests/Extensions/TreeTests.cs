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
            ITree<Section> tree = _sections.ToTree((parent, child) => child.ParentId == parent.Id, 3);

            // Assert root element
            tree.Children.Count.Should().Be(6);         // should have this amount of children
            tree.Data.Should().BeNull();                // data should be null
            tree.Level.Should().Be(0);                  // level is top level
            tree.IsRoot.Should().BeTrue();              // this is the only root element in the whole tree
            tree.IsLeaf.Should().BeFalse();             // the root is not a leaf
                                                        // these are the expected values of the roots children:
            tree.Children.Select(c => c.Data.Name).Should().Equal("Performers", "Members", "Visitors", "Volunteers", "Suppliers", "Contractors");

            // Assert a specific sub-tree
            Orso.Arpa.Domain.Extensions.ITree<Orso.Arpa.Domain.Entities.Section> subtree;
            subtree = tree.Children.First();            // navigate to first element in root tree, which is "Performers"
            subtree = subtree.Children.Skip(1).First(); // skip "Conductor", which makes "Choir" the first in the sub list now
            subtree.Children.Count.Should().Be(2);      // the "Choir" has two sub nodes called "Female Voices" and "Male Voices"
            subtree.Children.Select(c => c.Data.Name).Should().Equal("Female Voices", "Male Voices");

            // Assert a last node in root tree, which is "Contractors"
            subtree = tree.Children.Last();             // get the "Contractors" node
            subtree.Children.Count.Should().Be(0);      // currently no sub nodes defined
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
            ITree<Section> node = flattenedTree.First(node => node.Data.Name == "Alto");

            // Act
            List<Section> parents = node.GetParents();

            // Assert
            parents.Select(p => p.Name).Should().Equal("Low Female Voices", "Female Voices", "Choir", "Performers");
        }
    }
}