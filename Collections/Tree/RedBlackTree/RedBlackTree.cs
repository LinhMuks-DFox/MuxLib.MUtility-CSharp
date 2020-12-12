using System;

namespace MuxLib.MUtility.Collections.Tree.RedBlackTree
{
    public sealed class RedBlackTree<K, V>
        where K : IComparable
    {
        private enum Color { Red, Black }
        private class Node
        {
            public K Key { set; get; }
            public V Value { set; get; }
            public Node Left { set; get; } = null;
            public Node Right { set; get; } = null;
            public Color Color { set; get; } = Color.Red;
            public Node(K key, V value)
            {
                Key = key;
                Value = value;
            }
        }


        private Node _root;
        private int _size;

        public RedBlackTree()
        {
            _root = null;
            _size = 0;
        }

        public V this[K key]
        {
            get
            {
                Node node = FindNode(_root, key);
                return node == null ? default : node.Value;
            }
            set
            {
                Node node = FindNode(_root, key);
                if (node == null)
                    throw new Errors.InvalidArgumentError($"{key} is dose not exist.");
                node.Value = value;
            }
        }

        private Node FindNode(Node node, K key)
        {
            if (node == null)
                return null;

            if (key.Equals(key))
                return node;
            else if (key.CompareTo(node.Key) < 0)
                return FindNode(node.Left, key);
            else
                return FindNode(node.Right, key);
        }

        public int Size { get => _size; }

        public bool Empty { get => _size == 0; }

        private static bool IsRed(Node node)
        {
            if (node == null)
                return false; // Black(null node should be black.)
            return node.Color == Color.Red;
        }


        public void Append(K key, V value)
        {
            _root = Append(_root, key, value);
            _root.Color = Color.Black;
        }


        private Node Append(Node node, K key, V value)
        {


            if (node == null)
            {
                _size++;
                return new Node(key, value);
            }

            if (key.CompareTo(node.Key) < 0)
                node.Left = Append(node.Left, key, value);
            else if (key.CompareTo(node.Key) > 0)
                node.Right = Append(node.Right, key, value);
            else // key.CompareTo(node.key) == 0
                node.Value = value;

            if (IsRed(node.Right) && !IsRed(node.Left))
                node = LeftRotate(node);

            if (IsRed(node.Left) && IsRed(node.Left.Left))
                node = RightRotate(node);

            if (IsRed(node.Left) && IsRed(node.Right))
                FlipColors(node);

            return node;
        }

        private static Node RightRotate(Node node)
        {

            Node x = node.Left;
            node.Left = x.Right;
            x.Right = node;

            x.Color = node.Color;
            node.Color = Color.Red;

            return x;
        }
        private static Node LeftRotate(Node node)
        {

            Node x = node.Right;
            node.Right = x.Left;
            x.Left = node;

            x.Color = node.Color;
            node.Color = Color.Red;

            return x;
        }

        private static void FlipColors(Node node)
        {

            node.Color = Color.Red;
            node.Left.Color = Color.Black;
            node.Right.Color = Color.Black;
        }


        public bool Contains(K e)
        {
            return Contains(_root, e);
        }


        private bool Contains(Node node, K e)
        {

            if (node == null)
                return false;

            if (e.CompareTo(node.Key) == 0)
                return true;
            else if (e.CompareTo(node.Key) < 0)
                return Contains(node.Left, e);
            else // e.CompareTo(node.e) > 0
                return Contains(node.Right, e);
        }


        private Node Minimum(Node node)
        {
            if (node.Left == null)
                return node;
            return Minimum(node.Left);
        }

        private Node RemoveMin(Node node)
        {

            if (node.Left == null)
            {
                Node rightNode = node.Right;
                node.Right = null;
                _size--;
                return rightNode;
            }

            node.Left = RemoveMin(node.Left);
            return node;
        }


        public V Remove(K key)
        {
            Node node = FindNode(_root, key);
            if (node != null)
            {
                _root = Remove(_root, key);
                return node.Value;
            }
            return default;
        }


        private Node Remove(Node node, K key)
        {

            if (node == null)
                return null;

            if (key.CompareTo(node.Key) < 0)
            {
                node.Left = Remove(node.Left, key);
                return node;
            }
            else if (key.CompareTo(node.Key) > 0)
            {
                node.Right = Remove(node.Right, key);
                return node;
            }
            else
            {
                if (node.Left == null)
                {
                    Node rightNode = node.Right;
                    node.Right = null;
                    _size--;
                    return rightNode;
                }

                if (node.Right == null)
                {
                    Node leftNode = node.Left;
                    node.Left = null;
                    _size--;
                    return leftNode;
                }

                Node successor = Minimum(node.Right);
                successor.Right = RemoveMin(node.Right);
                successor.Left = node.Left;

                node.Left = node.Right = null;

                return successor;
            }
        }
    }
}
