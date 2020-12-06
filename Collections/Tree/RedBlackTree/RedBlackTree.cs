using System;
using System.Collections.Generic;
using System.Text;

namespace MuxLib.MUtility.Collections.Tree.RedBlackTree
{
    public class RedBlackTree<E>
        where E : IComparable
    {
        private enum Color { Red, Black }
        private class Node
        {
            public E e;
            public Node Left { set; get; } = null;
            public Node Right { set; get; } = null;
            public Color Color { set; get; } = Color.Red;
            public Node(E e)
            {
                this.e = e;
            }
        }

        private Node _root;
        private int _size;

        public RedBlackTree()
        {
            _root = null;
            _size = 0;
        }

        public int Size { get => _size; }

        public bool isEmpty { get => _size == 0; }

        private bool IsRed(Node node)
        {
            if (node == null)
                return false; // Black
            return node.Color == Color.Red ? true : false;
        }


        public void Add(E e)
        {
            _root = Add(_root, e);
        }


        private Node Add(Node node, E e)
        {

            if (node == null)
            {
                _size++;
                return new Node(e);
            }

            if (e.CompareTo(node.e) < 0)
                node.Left = Add(node.Left, e);
            else if (e.CompareTo(node.e) > 0)
                node.Right = Add(node.Right, e);

            return node;
        }


        public bool Contains(E e)
        {
            return Contains(_root, e);
        }


        private bool Contains(Node node, E e)
        {

            if (node == null)
                return false;

            if (e.CompareTo(node.e) == 0)
                return true;
            else if (e.CompareTo(node.e) < 0)
                return Contains(node.Left, e);
            else // e.CompareTo(node.e) > 0
                return Contains(node.Right, e);
        }



        public E Minimum()
        {
            if (_size == 0)
                throw new Errors.InvalidArgumentError("BST is empty!");

            return Minimum(_root).e;
        }


        private Node Minimum(Node node)
        {
            if (node.Left == null)
                return node;
            return Minimum(node.Left);
        }

        public E Maximum()
        {
            if (_size == 0)
                throw new Errors.InvalidArgumentError("BST is empty");

            return Maximum(_root).e;
        }


        private Node Maximum(Node node)
        {
            if (node.Right == null)
                return node;

            return Maximum(node.Right);
        }

        public E RemoveMin()
        {
            E ret = Minimum();
            _root = RemoveMin(_root);
            return ret;
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


        public E RemoveMax()
        {
            E ret = Maximum();
            _root = RemoveMax(_root);
            return ret;
        }


        private Node RemoveMax(Node node)
        {

            if (node.Right == null)
            {
                Node leftNode = node.Left;
                node.Left = null;
                _size--;
                return leftNode;
            }

            node.Right = RemoveMax(node.Right);
            return node;
        }


        public void Remove(E e)
        {
            _root = Remove(_root, e);
        }


        private Node Remove(Node node, E e)
        {

            if (node == null)
                return null;

            if (e.CompareTo(node.e) < 0)
            {
                node.Left = Remove(node.Left, e);
                return node;
            }
            else if (e.CompareTo(node.e) > 0)
            {
                node.Right = Remove(node.Right, e);
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


        public override string ToString()
        {
            StringBuilder res = new StringBuilder();
            GenerateBSTString(_root, 0, res);
            return res.ToString();
        }


        private void GenerateBSTString(Node node, int depth, StringBuilder res)
        {

            if (node == null)
            {
                res.Append(GenerateDepthString(depth) + "null\n");
                return;
            }

            res.Append(GenerateDepthString(depth) + node.e + "\n");
            GenerateBSTString(node.Left, depth + 1, res);
            GenerateBSTString(node.Right, depth + 1, res);
        }

        private string GenerateDepthString(int depth)
        {
            StringBuilder res = new StringBuilder();
            for (int i = 0; i < depth; i++)
                res.Append("--");
            return res.ToString();
        }
    }
}
