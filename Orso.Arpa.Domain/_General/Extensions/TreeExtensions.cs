using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// https://medium.com/@dmitry.pavlov/tree-structure-in-ef-core-how-to-configure-a-self-referencing-table-and-use-it-53effad60bf
/// </summary>
namespace Orso.Arpa.Domain.General.Extensions
{
    public interface ITree<T>
    {
        T Data { get; }
        ITree<T> Parent { get; }
        ICollection<ITree<T>> Children { get; }
        bool IsRoot { get; }
        bool IsLeaf { get; }
        int Level { get; }
    }

    /// <summary> Internal implementation of <see cref="ITree{T}" /></summary>
    /// <typeparam name="T">Custom data type to associate with tree node.</typeparam>
    internal class Tree<T> : ITree<T>
    {
        public T Data { get; }
        public ITree<T> Parent { get; private set; }
        public ICollection<ITree<T>> Children { get; }

        public bool IsRoot => Parent == null;
        public bool IsLeaf => Children.Count == 0;
        public int Level => IsRoot ? 0 : Parent.Level + 1;

        private Tree(T data)
        {
            Children = new LinkedList<ITree<T>>();
            Data = data;
        }

        public static Tree<T> FromLookup(ILookup<T, T> lookup, int? maxLevel)
        {
            T rootData = lookup.Count == 1 ? lookup.First().Key : default;
            var root = new Tree<T>(rootData);

            root.LoadChildren(lookup, maxLevel);

            return root;
        }

        private void LoadChildren(ILookup<T, T> lookup, int? maxLevel)
        {
            foreach (T data in lookup[Data])
            {
                var child = new Tree<T>(data) { Parent = this };
                if (maxLevel is null || child.Level <= maxLevel)
                {
                    Children.Add(child);
                    child.LoadChildren(lookup, maxLevel);
                }
            }
        }
    }

    public static class TreeExtensions
    {
        /// <summary> Flatten tree to plain list of nodes </summary>
        public static IEnumerable<TNode> Flatten<TNode>(this IEnumerable<TNode> nodes, Func<TNode, IEnumerable<TNode>> childrenSelector)
        {
            ArgumentNullException.ThrowIfNull(nodes);

            return nodes.SelectMany(c => childrenSelector(c).Flatten(childrenSelector)).Concat(nodes);
        }

        /// <summary> Converts given list to tree. </summary>
        /// <typeparam name="T">Custom data type to associate with tree node.</typeparam>
        /// <param name="items">The collection items.</param>
        /// <param name="parentSelector">Expression to select parent.</param>
        public static ITree<T> ToTree<T>(this IList<T> items, Func<T, T, bool> parentSelector, int? maxLevel = null)
        {
            ArgumentNullException.ThrowIfNull(items);

            ILookup<T, T> lookup = items.ToLookup(item => items.FirstOrDefault(parent => parentSelector(parent, item)), child => child);
            return Tree<T>.FromLookup(lookup, maxLevel);
        }

        public static List<T> GetParents<T>(this ITree<T> node, List<T> parentNodes = null) where T : class
        {
            while (true)
            {
                parentNodes ??= new List<T>();
                if (node?.Parent?.Data == null)
                {
                    return parentNodes;
                }

                parentNodes.Add(node.Parent.Data);
                node = node.Parent;
            }
        }
    }
}
