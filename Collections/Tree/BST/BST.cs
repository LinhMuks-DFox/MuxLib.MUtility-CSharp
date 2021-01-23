using System;
using System.Collections.Generic;
using MuxLib.MUtility.Collections.Metas.ABClass;

namespace MuxLib.MUtility.Collections.Tree.BST
{
    public sealed class Bst<TK, TV> : ABCOrderSymbolTable<TK, TV>
        where TK : IComparable
    {
        public Bst()
        {
            Root = null;
        }

        public override bool Empty => Size == 0;

        public override int Size => NodeSize(Root);

        private Node Root { set; get; }

        public override TV this[TK key]
        {
            get => Get(Root, key);
            set => Root = Set(Root, key, value);
        }

        private int NodeSize(Node node)
        {
            return node?.N ?? 0;
        }

        private static TV Get(Node node, TK key)
        {
            if (node == null)
                return default;
            var cmp = key.CompareTo(node.Key);
            return cmp switch
            {
                > 0 => Get(node.Left, key),
                < 0 => Get(node.Right, key),
                _ => node.Value
            };
        }

        private Node Set(Node node, TK key, TV value)
        {
            if (node == null)
                return new Node(key, value, 1);
            var cmp = key.CompareTo(node.Key);
            switch (cmp)
            {
                case < 0:
                    node.Left = Set(node.Left, key, value);
                    break;
                case > 0:
                    node.Right = Set(node.Right, key, value);
                    break;
                default:
                    node.Value = value;
                    break;
            }

            node.N = NodeSize(node.Left) + NodeSize(node.Right) + 1;
            return node;
        }

        public override void Remove(TK key)
        {
            Root = Remove(Root, key);
        }

        private Node Remove(Node node, TK key)
        {
            if (node == null)
                return null;
            var cmp = key.CompareTo(node.Key);

            switch (cmp)
            {
                case < 0:
                    node.Left = Remove(node.Left, key);
                    break;
                case > 0:
                    node.Right = Remove(node.Right, key);
                    break;
                default:
                {
                    if (node.Right == null)
                        return node.Left;
                    if (node.Left == null)
                        return node.Right;
                    var t = node;
                    node = Min(t.Right);
                    node.Right = DeleteMin(t.Right);
                    node.Left = t.Left;
                    break;
                }
            }

            node.N = NodeSize(node.Left) + NodeSize(node.Right) + 1;
            return node;
        }

        public override bool Contains(TK key)
        {
            return Get(Root, key) == null;
        }

        public override TK Min()
        {
            return Min(Root).Key;
        }

        private Node Min(Node node)
        {
            return node.Left == null ? node : Min(node.Left);
        }

        public override TK Max()
        {
            return Max(Root).Key;
        }

        private Node Max(Node node)
        {
            return node.Right == null ? node : Max(node.Right);
        }

        public override TK Floor(TK key)
        {
            var x = Floor(Root, key);
            return x == null ? default : x.Key;
        }

        private static Node Floor(Node node, TK key)
        {
            if (node == null) return null;
            var cmp = key.CompareTo(node.Key);
            switch (cmp)
            {
                case 0:
                    return node;
                case < 0:
                    return Floor(node.Left, key);
            }

            var n = Floor(node.Right, key);
            return n ?? node;
        }

        public override TK Ceiling(TK key)
        {
            var x = Ceiling(Root, key);
            return x == null ? default : x.Key;
        }

        private Node Ceiling(Node node, TK key)
        {
            if (node == null)
                return null;
            var cmp = key.CompareTo(node.Key);
            switch (cmp)
            {
                case 0:
                    return node;
                case > 0:
                    return Ceiling(node.Left, key);
            }

            var n = Ceiling(node.Left, key);
            return n ?? node;
        }

        public override int Rank(TK key)
        {
            return Rank(Root, key);
        }

        private int Rank(Node node, TK key)
        {
            if (node == null)
                return 0;
            var cmp = key.CompareTo(node.Key);

            return cmp switch
            {
                < 0 => Rank(node.Left, key),
                > 0 => 1 + NodeSize(node.Left) + Rank(node.Right, key),
                _ => NodeSize(node.Left)
            };
        }

        public override TK Select(int k)
        {
            return Select(Root, k).Key;
        }

        private Node Select(Node node, int k)
        {
            if (node == null)
                return null;
            var t = NodeSize(node.Left);
            if (t <= k) return t < k ? Select(node.Right, k - t - 1) : node;

            return Select(node.Left, k);
        }

        public override void DeleteMin()
        {
            Root = DeleteMin(Root);
        }

        private Node DeleteMin(Node node)
        {
            if (node.Left == null)
                return node.Right;
            node.Left = DeleteMin(node.Left);
            node.N = NodeSize(node.Left) + NodeSize(node.Right) + 1;
            return node;
        }

        public override void DeleteMax()
        {
            Root = DeleteMax(Root);
        }

        private Node DeleteMax(Node node)
        {
            if (node.Right == null)
                return node.Left;
            node.Right = DeleteMax(node.Right);
            node.N = NodeSize(node.Right) + NodeSize(node.Left) + 1;
            return node;
        }

        public override int NumberOf(TK low, TK max)
        {
            return new Queue<TK>(SortedKeys(low, max)).Count;
        }

        public override IEnumerable<TK> SortedKeys(TK low, TK max)
        {
            return SortedKeys(Root, Min(), Max());
        }

        private IEnumerable<TK> SortedKeys(Node node, TK low, TK max)
        {
            var queue = new Queue<TK>(node.N);
            SortedKeys(node, queue, low, max);
            return queue;
        }

        private void SortedKeys(Node node, Queue<TK> queue, TK low, TK max)
        {
            if (node == null)
                return;
            var cmplow = low.CompareTo(node.Key);
            var cmpmax = max.CompareTo(node.Key);
            if (cmplow < 0)
                SortedKeys(node.Left, queue, low, max);
            if (cmplow <= 0 && cmpmax >= 0)
                queue.Enqueue(node.Key);
            if (cmpmax > 0)
                SortedKeys(node.Right, queue, low, max);
        }

        private class Node
        {
            public Node(TK key, TV value, int n)
            {
                (Key, Value, N) = (key, value, n);
            }

            public TK Key { get; }

            public TV Value { set; get; }

            public Node Left { set; get; }

            public Node Right { set; get; }

            /// <summary>
            ///     How many nodes are there in the tree with the current node as the root node
            /// </summary>
            public int N { set; get; }
        }
    }
}