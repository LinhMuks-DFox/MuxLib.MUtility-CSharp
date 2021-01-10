using System;
using System.Collections.Generic;

namespace MuxLib.MUtility.Collections.Tree.BST
{
    public sealed class BST<K, V> : Metas.ABClass.ABCOrderSymbolTable<K, V>
        where K : IComparable
    {
        private class Node
        {
            public K Key { set; get; }

            public V Value { set; get; }

            public Node Left { set; get; } = null;

            public Node Right { set; get; } = null;

            /// <summary>
            /// How many nodes are there in the tree with the current node as the root node
            /// </summary>
            public int N { set; get; }

            public Node(K key, V value, int N)
            {
                (Key, Value, this.N) = (key, value, N);
            }
        }

        private int NodeSize(Node node)
        {
            if (node == null) return 0;
            else return node.N;
        }
        public override bool Empty { get => Size == 0; }

        public override int Size { get => NodeSize(_root); }

        private Node _root;

        public BST()
        {
            _root = null;
        }

        public override V this[K key]
        {
            get => Get(_root, key);
            set { _root = Set(_root, key, value); }
        }

        private V Get(Node node, K key)
        {
            if (node == null)
                return default;
            int cmp = key.CompareTo(node.Key);
            if (cmp > 0) return Get(node.Left, key);
            else if (cmp < 0) return Get(node.Right, key);
            else return node.Value;
        }

        private Node Set(Node node, K key, V value)
        {
            if (node == null)
                return new Node(key, value, 1);
            int cmp = key.CompareTo(node.Key);
            if (cmp < 0) node.Left = Set(node.Left, key, value);
            else if (cmp > 0) node.Right = Set(node.Right, key, value);
            else
                node.Value = value;
            node.N = NodeSize(node.Left) + NodeSize(node.Right) + 1;
            return node;
        }

        public override void Remove(K key)
        {
            _root = Remove(_root, key);
        }

        private Node Remove(Node node, K key)
        {
            if (node == null) return null;
            int cmp = key.CompareTo(node.Key);

            if (cmp < 0) node.Left = Remove(node.Left, key);
            else if (cmp > 0) node.Right = Remove(node.Right, key);
            else
            {
                if (node.Right == null) return node.Left;
                if (node.Left == null) return node.Right;
                Node t = node;
                node = Min(t.Right);
                node.Right = DeleteMin(t.Right);
                node.Left = t.Left;
            }

            node.N = NodeSize(node.Left) + NodeSize(node.Right) + 1;
            return node;

        }

        public override bool Contains(K key)
        {
            return Get(_root, key) == null;
        }

        public override K Min()
        {
            return Min(_root).Key;
        }

        private Node Min(Node node)
        {
            if (node.Left == null)
                return node;
            return Min(node.Left);
        }

        public override K Max()
        {
            return Max(_root).Key;
        }

        private Node Max(Node node)
        {
            if (node.Right == null)
                return node;
            return Max(node.Right);
        }

        public override K Floor(K key)
        {
            Node x = Floor(_root, key);
            if (x == null) return default;
            return x.Key;
        }

        private Node Floor(Node node, K key)
        {
            if (node == null) return null;
            int cmp = key.CompareTo(node.Key);
            if (cmp == 0) return node;
            if (cmp < 0) return Floor(node.Left, key);
            Node n = Floor(node.Right, key);
            if (n != null) return n;
            else return node;
        }

        public override K Ceiling(K key)
        {
            Node x = Ceiling(_root, key);
            if (x == null) return default;
            return x.Key;
        }

        private Node Ceiling(Node node, K key)
        {
            if (node == null) return null;
            int cmp = key.CompareTo(node.Key);
            if (cmp == 0) return node;
            if (cmp > 0) return Ceiling(node.Left, key);
            Node n = Ceiling(node.Left, key);
            if (n != null) return n;
            else return node;
        }

        public override int Rank(K key)
        {
            return Rank(_root, key);
        }
        private int Rank(Node node, K key)
        {
            if (node == null) return 0;
            int cmp = key.CompareTo(node.Key);

            if (cmp < 0) return Rank(node.Left, key);
            else if (cmp > 0) return 1 + NodeSize(node.Left) + Rank(node.Right, key);
            else return NodeSize(node.Left);

        }

        public override K Select(int k)
        {
            return Select(_root, k).Key;
        }

        private Node Select(Node node, int k)
        {
            if (node == null) return null;
            int t = NodeSize(node.Left);
            if (t > k) return Select(node.Left, k);
            else if (t < k) return Select(node.Right, k - t - 1);
            else return node;
        }

        public override void DeleteMin()
        {
            _root = DeleteMin(_root);
        }

        private Node DeleteMin(Node node)
        {
            if (node.Left == null) return node.Right;
            node.Left = DeleteMin(node.Left);
            node.N = NodeSize(node.Left) + NodeSize(node.Right) + 1;
            return node;
        }

        public override void DeleteMax()
        {
            _root = DeleteMax(_root);
        }

        private Node DeleteMax(Node node)
        {
            if (node.Right == null) return node.Left;
            node.Right = DeleteMax(node.Right);
            node.N = NodeSize(node.Right) + NodeSize(node.Left) + 1;
            return node;
        }

        public override int NumberOf(K low, K max)
        {
            return new Queue<K>(SortedKeys(low, max)).Count;
        }

        public override IEnumerable<K> SortedKeys(K low, K max)
        {
            return SortedKeys(_root, Min(), Max());
        }

        private IEnumerable<K> SortedKeys(Node node, K low, K max)
        {
            Queue<K> queue = new Queue<K>(_root.N);
            SortedKeys(_root, queue, low, max);
            return queue;
        }

        private void SortedKeys(Node node, Queue<K> queue, K low, K max)
        {
            if (node == null) return;
            int cmplow = low.CompareTo(node.Key);
            int cmpmax = max.CompareTo(node.Key);
            if (cmplow < 0) SortedKeys(node.Left, queue, low, max);
            if (cmplow <= 0 && cmpmax >= 0) queue.Enqueue(node.Key);
            if (cmpmax > 0) SortedKeys(node.Right, queue, low, max);
        }
    }
}