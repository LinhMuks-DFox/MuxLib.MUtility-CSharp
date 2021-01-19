using System;
using System.Collections.Generic;
using MuxLib.MUtility.Collections.Metas.ABClass;
using MuxLib.MUtility.Com.Attr;

namespace MuxLib.MUtility.Collections.Tree.AVLTree
{
    [Unimplemented]
    public class R_AVLTree<K, V> : ABCOrderSymbolTable<K, V>
        where K : IComparable
    {
        public R_AVLTree()
        {
            Root = null;
        }

        private Node Root { get; }

        public override bool Empty => Size == 0;
        public override int Size => NodeSize(Root);

        public override V this[K key]
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        private static int NodeSize(Node node)
        {
            return node?.N ?? 0;
        }

        private static Node RightRotate(Node y)
        {
            Node x = y.Left, t3 = x.Right;
            x.Right = y;
            y.Left = t3;

            // Update Node's height
            y.N = Math.Max(NodeSize(y.Left), NodeSize(y.Right)) + 1;
            x.N = Math.Max(NodeSize(x.Left), NodeSize(x.Right)) + 1;
            return x;
        }

        private Node GetNode(Node node, K key)
        {
            if (node == null)
                return null;
            return key.CompareTo(node.Key) switch
            {
                0 => node,
                < 0 => GetNode(node.Left, key),
                _ => GetNode(node.Right, key)
            };
        }

        private static Node LeftRotate(Node y)
        {
            Node x = y.Right, t2 = x.Left;
            x.Left = y;
            y.Right = t2;
            y.N = Math.Max(NodeSize(y.Left), NodeSize(y.Right)) + 1;
            x.N = Math.Max(NodeSize(x.Left), NodeSize(x.Right)) + 1;
            return x;
        }

        private static int GetBalanceFactor(Node node)
        {
            if (node == null)
                return 0;
            return NodeSize(node.Left) - NodeSize(node.Right);
        }

        public override void Remove(K key)
        {
            throw new NotImplementedException();
        }

        public override bool Contains(K key)
        {
            throw new NotImplementedException();
        }

        public override K Min()
        {
            throw new NotImplementedException();
        }

        public override K Max()
        {
            throw new NotImplementedException();
        }

        public override K Floor(K key)
        {
            throw new NotImplementedException();
        }

        public override K Ceiling(K key)
        {
            throw new NotImplementedException();
        }

        public override int Rank(K key)
        {
            throw new NotImplementedException();
        }

        public override K Select(int k)
        {
            throw new NotImplementedException();
        }

        public override void DeleteMin()
        {
            throw new NotImplementedException();
        }

        public override void DeleteMax()
        {
            throw new NotImplementedException();
        }

        public override int NumberOf(K low, K max)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<K> SortedKeys(K low, K max)
        {
            throw new NotImplementedException();
        }

        private class Node
        {
            public Node(K key, V value, int n)
            {
                (Key, Value, N) = (key, value, n);
            }

            public int N { set; get; }
            public Node Left { set; get; }
            public Node Right { set; get; }
            public K Key { get; }
            public V Value { get; }
        }
    }
}