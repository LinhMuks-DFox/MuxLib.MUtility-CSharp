using System;
using System.Collections.Generic;

namespace MuxLib.MUtility.Collections.Tree.AVLTree
{
    public sealed class AVLTree<K, V>
        where K : IComparable
    {
        private class Node
        {
            public K Key { get; set; }
            public V Value { get; set; }
            public Node Left { set; get; } = null;
            public Node Right { set; get; } = null;
            public int Height { set; get; } = 1;

            public Node(K key, V value)
            {
                Key = key; Value = value;
            }
        }

        private Node _root;
        private int _size;

        public int Size { get => _size; }
        public bool IsEmpty { get => Size == 0; }

        public AVLTree()
        {
            _root = null;
            _size = 0;
        }

        public V this[K key] { get { return Get(key); } set { Set(key, value); } }

        private static int GetHeight(Node node)
        {
            if (node == null)
                return 0;
            return node.Height;
        }

        public void Append(K key, V value)
        {
            _root = Append(_root, key, value);
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
            else
                node.Value = value;
            // Update Height
            node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));
            int balance_factor = GetBalanceFactor(node);
#if DEBUG
            if (Math.Abs(balance_factor) > 1)
                Console.WriteLine($"Unbalance: {balance_factor}");
#endif
            // Balance maintenance

            // LL
            if (balance_factor > 1 && GetBalanceFactor(node.Left) >= 0)
                return RightRotate(node);
            // RR
            if (balance_factor < -1 && GetBalanceFactor(node.Right) <= 0)
                return LeftRotate(node);
            // LR
            if (balance_factor > 1 && GetBalanceFactor(node.Left) < 0)
            {
                node.Left = LeftRotate(node.Left); // Convert to LL
                return RightRotate(node);
            }

            // RL
            if (balance_factor < -1 && GetBalanceFactor(node.Right) > 0)
            {
                node.Right = RightRotate(node); // Convert to RR
                return LeftRotate(node);
            }
            return node;

        }
        /*
            //          y                                x
            //         / \                             /   \
            //        x   T4        RightRotate       z     y
            //       / \            -------->        / \   / \
            //      z   T3                          T1 T2 T3 T4
            //     / \
            //    T1  T2
        */
        private static Node RightRotate(Node y)
        {
            Node x = y.Left, t3 = x.Right;
            x.Right = y; y.Left = t3;

            // Update Node's height
            y.Height = Math.Max(GetHeight(y.Left), GetHeight(y.Right)) + 1;
            x.Height = Math.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;
            return x;
        }
        /*
            //          y                                 x
            //         / \                              /   \
            //        T1  x         LeftRotate         y     z
            //           / \        -------->         / \   / \
            //          T2  z                        T1 T2 T3 T4
            //             / \
            //            T3  T4
        */
        private static Node LeftRotate(Node y)
        {
            Node x = y.Right, T2 = x.Left;
            x.Left = y; y.Right = T2;
            y.Height = Math.Max(GetHeight(y.Left), GetHeight(y.Right)) + 1;
            x.Height = Math.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;
            return x;
        }


        private static int GetBalanceFactor(Node node)
        {
            if (node == null)
                return 0;
            return GetHeight(node.Left) - GetHeight(node.Right);
        }

        public bool IsBST()
        {
            List<K> keys = new List<K>();
            InOrder(_root, keys);
            for (int i = 1; i < keys.Count; i++)
            {
                if (keys[i - 1].CompareTo(keys[i]) > 0)
                    return false;
            }
            return true;
        }

        public bool IsBalanced()
        {
            return IsBalanced(_root);
        }

        private bool IsBalanced(Node node)
        {
            if (node == null)
                return true;
            int balanced_factor = GetBalanceFactor(node);
            if (Math.Abs(balanced_factor) > 1)
                return false;
            return IsBalanced(node.Left) && IsBalanced(node.Right);
        }

        private void InOrder(Node node, List<K> keys)
        {
            if (node == null)
                return;
            InOrder(node.Left, keys);
            keys.Add(node.Key);
            InOrder(node.Right, keys);
        }

        public void Set(K key, V value)
        {
            Node node = GetNode(_root, key);
            if (node == null)
                throw new Errors.InvalidArgumentError($"{key} dose not exist");
            node.Value = value;
        }

        public V Get(K key)
        {
            Node node = GetNode(_root, key);
            if (node == null)
                throw new Errors.InvalidArgumentError($"{key} dose not exist");
            return node.Value;
        }

        private Node GetNode(Node node, K key)
        {
            if (node == null)
                return null;
            if (key.Equals(node.Key))
                return node;
            else if (key.CompareTo(node.Key) < 0)
                return GetNode(node.Left, key);
            else
                return GetNode(node.Right, key);
        }

        public V Remove(K key)
        {
            Node node = GetNode(_root, key);
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

            Node retNode;
            if (key.CompareTo(node.Key) < 0)
            {
                node.Left = Remove(node.Left, key);
                retNode = node;
            }
            else if (key.CompareTo(node.Key) > 0)
            {
                node.Right = Remove(node.Right, key);
                retNode = node;
            }
            else
            {
                if (node.Left == null)
                {
                    Node right_node = node.Right;
                    node.Right = null;
                    _size--;
                    retNode = right_node;
                }
                else if (node.Right == null)
                {
                    Node left_node = node.Left;
                    node.Left = null;
                    _size--;
                    retNode = left_node;
                }
                else
                {
                    Node successor = Minimum(node.Right);
                    successor.Right = Remove(node.Right, successor.Key);
                    successor.Left = node.Left;
                    node.Left = node.Right = null;
                    retNode = successor;
                }
            }
            if (retNode == null)
                return null;
            retNode.Height = 1 + Math.Max(GetHeight(retNode.Left), GetHeight(retNode.Right));
            int balanceFactor = GetBalanceFactor(retNode);

            if (balanceFactor > 1 && GetBalanceFactor(retNode.Left) >= 0)
                return RightRotate(retNode);

            // RR
            if (balanceFactor < -1 && GetBalanceFactor(retNode.Right) <= 0)
                return LeftRotate(retNode);

            // LR
            if (balanceFactor > 1 && GetBalanceFactor(retNode.Left) < 0)
            {
                retNode.Left = LeftRotate(retNode.Left);
                return RightRotate(retNode);
            }

            // RL
            if (balanceFactor < -1 && GetBalanceFactor(retNode.Right) > 0)
            {
                retNode.Right = RightRotate(retNode.Right);
                return LeftRotate(retNode);
            }

            return retNode;
        }


        private Node Minimum(Node node)
        {
            if (node.Left == null)
                return node;
            return Minimum(node.Left);
        }

        private Node Maximum(Node node)
        {
            if (node.Right == null)
                return node;
            return Maximum(node.Right);
        }

        public bool Contains(K key)
        {
            return GetNode(_root, key) != null;
        }
    }
}
