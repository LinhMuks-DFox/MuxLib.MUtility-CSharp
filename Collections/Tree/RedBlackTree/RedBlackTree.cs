﻿using System;
using System.Collections.Generic;

namespace MuxLib.MUtility.Collections.Tree.RedBlackTree
{
    public sealed class RedBlackTree<K, V> : Metas.ABClass.ABCOrderSymbolTable<K, V>
        where K : IComparable
    {
        private enum Color { Red, Black }

        private class Node
        {
            public K Key { set; get; }

            public V Value { set; get; }

            public Node Left { set; get; } = null;

            public Node Right { set; get; } = null;

            public int N { set; get; }

            public Color Color { set; get; }

            public Node(K key, V value, int N, Color color)
            {
                (Key, Value, this.N, Color) = (key, value, N, color);
            }

        }

        private bool IsRed(Node node)
        {
            if (node == null)
                return false;
            return node.Color == Color.Red;
        }

        private void FlipColor(Node node)
        {
            node.Color = Color.Red;
            node.Left.Color = Color.Black;
            node.Right.Color = Color.Black;
        }
        private Node LeftRotate(Node node)
        {
            Node x = node.Right;
            node.Right = x.Left;
            x.Left = node;
            x.Color = node.Color;
            node.Color = Color.Red;
            x.N = node.N;
            node.N = 1 + NodeSize(node.Left) + NodeSize(node.Right);
            return x;
        }

        private Node RightRotate(Node node)
        {
            Node x = node.Left;
            node.Left = x.Right;
            x.Right = node;
            x.Color = node.Color;
            node.Color = Color.Red;
            x.N = node.N;
            node.N = 1 + NodeSize(node.Left) + NodeSize(node.Right);
            return x;
        }
        private int NodeSize(Node node)
        {
            if (node == null)
                return 0;
            else
                return node.N;
        }
        public override V this[K key]
        {
            get => Get(_root, key);
            set
            {
                _root = Set(_root, key, value);
                _root.Color = Color.Black;
            }
        }


        private V Get(Node node, K key)
        {
            if (node == null)
                return default;
            int cmp = key.CompareTo(node.Key);
            if (cmp > 0)
                return Get(node.Left, key);
            else if (cmp < 0)
                return Get(node.Right, key);
            else
                return node.Value;
        }

        private Node Set(Node h, K key, V value)
        {
            if (h == null)
                return new Node(key, value, 1, Color.Red);
            int cmp = key.CompareTo(h.Key);

            if (cmp < 0)
                h.Left = Set(h.Left, key, value);
            else if (cmp > 0)
                h.Right = Set(h.Right, key, value);
            else
                h.Value = value;

            if (IsRed(h.Right) && !IsRed(h.Left))
                h = LeftRotate(h);

            if (IsRed(h.Left) && IsRed(h.Left))
                h = RightRotate(h);

            if (IsRed(h.Left) && IsRed(h.Right))
                FlipColor(h);

            h.N = NodeSize(h.Left) + NodeSize(h.Right) + 1;

            return h;
        }

        public override bool Empty => Size == 0;

        public override int Size => NodeSize(_root);

        private Node _root;
        public RedBlackTree()
        {
            _root = null;
        }

        public override K Ceiling(K key)
        {
            throw new NotImplementedException();
        }

        public override bool Contains(K key)
        {
            throw new NotImplementedException();
        }

        public override void DeleteMax()
        {
            throw new NotImplementedException();
        }

        public override void DeleteMin()
        {
            throw new NotImplementedException();
        }

        public override K Floor(K key)
        {
            throw new NotImplementedException();
        }

        public override K Max()
        {
            return Max(_root).Key;
        }

        private Node Max(Node node)
        {
            if (node == null)
                return node;
            return Max(node.Right);
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

        public override int NumberOf(K low, K max)
        {
            return new Queue<K>(SortedKeys(low, max)).Count;
        }

        public override int Rank(K key)
        {
            return Rank(_root, key);
        }
        private int Rank(Node node, K key)
        {
            if (node == null)
                return 0;
            int cmp = key.CompareTo(node.Key);

            if (cmp < 0)
                return Rank(node.Left, key);
            else if (cmp > 0)
                return 1 + NodeSize(node.Left) + Rank(node.Right, key);
            else
                return NodeSize(node.Left);

        }

        public override void Remove(K key)
        {
            throw new NotImplementedException();
        }
        public override K Select(int k)
        {
            return Select(_root, k).Key;
        }

        private Node Select(Node node, int k)
        {
            if (node == null)
                return null;
            int t = NodeSize(node.Left);
            if (t > k)
                return Select(node.Left, k);
            else if (t < k)
                return Select(node.Right, k - t - 1);
            else return node;
        }

        public override IEnumerable<K> SortedKeys(K low, K max)
        {
            return SortedKeys(_root, Min(), Max());
        }

        private IEnumerable<K> SortedKeys(Node node, K low, K max)
        {
            Queue<K> queue = new Queue<K>(node.N);
            SortedKeys(node, queue, low, max);
            return queue;
        }

        private void SortedKeys(Node node, Queue<K> queue, K low, K max)
        {
            if (node == null)
                return;
            int cmplow = low.CompareTo(node.Key);
            int cmpmax = max.CompareTo(node.Key);
            if (cmplow < 0)
                SortedKeys(node.Left, queue, low, max);
            if (cmplow <= 0 && cmpmax >= 0)
                queue.Enqueue(node.Key);
            if (cmpmax > 0)
                SortedKeys(node.Right, queue, low, max);
        }
    }
}
