using System;
using MuxLib.MUtility.Collections.Errors;

namespace MuxLib.MUtility.Collections.Tree.AVLTree
{
    /// <summary>
    ///     K 可以不必是一个Compareable 的数组，可以new GenericAVLTree.CompareElements()
    ///     lambda函数
    /// </summary>
    /// <typeparam name="K"></typeparam>
    /// <typeparam name="V"></typeparam>
    public sealed class GenericAVLTree<K, V>
    {
        public delegate int CompareElements(K key1, K key2);

        private Node _root;

        public GenericAVLTree(CompareElements compare)
        {
            _root = null;
            Size = 0;
            Compare = compare;
        }

        private CompareElements Compare { get; }

        public int Size { get; private set; }

        public bool IsEmpty => Size == 0;

        public V this[K key]
        {
            get
            {
                var node = GetNode(_root, key);
                if (node == null)
                    throw new InvalidArgumentError($"{key} dose not exist");
                return node.Value;
            }
            set
            {
                var node = GetNode(_root, key);
                if (node == null)
                    throw new InvalidArgumentError($"{key} dose not exist");
                node.Value = value;
            }
        }

        private static int GetHeight(Node node)
        {
            if (node == null)
                return 0;
            return node.Height;
        }

        private Node GetNode(Node node, K key)
        {
            if (node == null)
                return null;
            if (key.Equals(node.Key))
                return node;
            if (Compare(key, node.Key) < 0)
                return GetNode(node.Left, key);
            return GetNode(node.Right, key);
        }

        private int GetBalanceFactor(Node node)
        {
            if (node == null)
                return 0;
            return GetHeight(node.Left) - GetHeight(node.Right);
        }

        private static Node RightRotate(Node y)
        {
            Node x = y.Left, t3 = x.Right;
            x.Right = y;
            y.Left = t3;

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
            x.Left = y;
            y.Right = T2;
            y.Height = Math.Max(GetHeight(y.Left), GetHeight(y.Right)) + 1;
            x.Height = Math.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;
            return x;
        }

        private Node Append(Node node, K key, V value)
        {
            if (node == null)
            {
                Size++;
                return new Node(key, value);
            }

            if (Compare(key, node.Key) < 0)
                node.Left = Append(node.Left, key, value);
            else if (Compare(key, node.Key) > 0)
                node.Right = Append(node.Right, key, value);
            else
                node.Value = value;
            // Update Height
            node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));
            var balance_factor = GetBalanceFactor(node);

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

        private Node Minimum(Node node)
        {
            if (node.Left == null)
                return node;
            return Minimum(node.Left);
        }

        private Node Remove(Node node, K key)
        {
            if (node == null)
                return null;

            Node retNode;
            if (Compare(key, node.Key) < 0)
            {
                node.Left = Remove(node.Left, key);
                retNode = node;
            }
            else if (Compare(key, node.Key) > 0)
            {
                node.Right = Remove(node.Right, key);
                retNode = node;
            }
            else
            {
                if (node.Left == null)
                {
                    var right_node = node.Right;
                    node.Right = null;
                    Size--;
                    retNode = right_node;
                }
                else if (node.Right == null)
                {
                    var left_node = node.Left;
                    node.Left = null;
                    Size--;
                    retNode = left_node;
                }
                else
                {
                    var successor = Minimum(node.Right);
                    successor.Right = Remove(node.Right, successor.Key);
                    successor.Left = node.Left;
                    node.Left = node.Right = null;
                    retNode = successor;
                }
            }

            if (retNode == null)
                return null;
            retNode.Height = 1 + Math.Max(GetHeight(retNode.Left), GetHeight(retNode.Right));
            var balanceFactor = GetBalanceFactor(retNode);

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

        public void Append(K key, V value)
        {
            _root = Append(_root, key, value);
        }

        public V Remove(K key)
        {
            var node = GetNode(_root, key);
            if (node != null)
            {
                _root = Remove(_root, key);
                return node.Value;
            }

            return default;
        }

        public bool Contains(K key)
        {
            return GetNode(_root, key) != null;
        }

        private class Node
        {
            public Node(K key, V value)
            {
                Key = key;
                Value = value;
            }

            public K Key { get; }
            public V Value { get; set; }
            public Node Left { set; get; }
            public Node Right { set; get; }
            public int Height { set; get; } = 1;
        }
    }
}