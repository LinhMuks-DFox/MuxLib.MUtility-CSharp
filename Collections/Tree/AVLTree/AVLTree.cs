using System;
using System.Collections.Generic;
using MuxLib.MUtility.Collections.Errors;
using MuxLib.MUtility.Collections.Metas.ABClass;

namespace MuxLib.MUtility.Collections.Tree.AVLTree
{
    public sealed class AvlTree<TK, TV> : ABCSymbolTable<TK, TV>
        where TK : IComparable
    {
        private int _size;


        public AvlTree()
        {
            Root = null;
            _size = 0;
        }

        private Node Root { set; get; }
        public override int Size => _size;
        public override bool Empty => Size == 0;

        public override TV this[TK key]
        {
            get => Get(key);
            set
            {
                var node = GetNode(Root, key);
                if (node == null)
                    Append(Root, key, value);
                else
                    node.Value = value;
            }
        }

        private static int GetHeight(Node node)
        {
            return node?.Height ?? 0;
        }

        public void Append(TK key, TV value)
        {
            Root = Append(Root, key, value);
        }

        private Node Append(Node node, TK key, TV value)
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
            var balanceFactor = GetBalanceFactor(node);
            // #if DEBUG
            //             if (Math.Abs(balance_factor) > 1)
            //                 Console.WriteLine($"Unbalance: {balance_factor}");
            // #endif
            // Balance maintenance

            // LL
            if (balanceFactor > 1 && GetBalanceFactor(node.Left) >= 0)
                return RightRotate(node);
            // RR
            if (balanceFactor < -1 && GetBalanceFactor(node.Right) <= 0)
                return LeftRotate(node);
            // LR
            if (balanceFactor > 1 && GetBalanceFactor(node.Left) < 0)
            {
                node.Left = LeftRotate(node.Left); // Convert to LL
                return RightRotate(node);
            }

            // RL
            if (balanceFactor < -1 && GetBalanceFactor(node.Right) > 0)
            {
                node.Right = RightRotate(node.Right); // Convert to RR
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
            Node x = y.Right, t2 = x.Left;
            x.Left = y;
            y.Right = t2;
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

        public bool IsBst()
        {
            var keys = new List<TK>();
            InOrder(Root, keys);
            for (var i = 1; i < keys.Count; i++)
                if (keys[i - 1].CompareTo(keys[i]) > 0)
                    return false;
            return true;
        }

        public bool IsBalanced()
        {
            return IsBalanced(Root);
        }

        private static bool IsBalanced(Node node)
        {
            if (node == null)
                return true;
            var balancedFactor = GetBalanceFactor(node);
            if (Math.Abs(balancedFactor) > 1)
                return false;
            return IsBalanced(node.Left) && IsBalanced(node.Right);
        }

        private static void InOrder(Node node, List<TK> keys)
        {
            if (node == null)
                return;
            InOrder(node.Left, keys);
            keys.Add(node.Key);
            InOrder(node.Right, keys);
        }

        public void Set(TK key, TV value)
        {
            var node = GetNode(Root, key);
            if (node == null)
                throw new InvalidArgumentError($"{key} dose not exist");
            node.Value = value;
        }

        private TV Get(TK key)
        {
            var node = GetNode(Root, key);
            if (node == null)
                throw new InvalidArgumentError($"{key} dose not exist");
            return node.Value;
        }

        private static Node GetNode(Node node, TK key)
        {
            if (node == null)
                return null;
            var cmp = key.CompareTo(node.Key);
            return cmp switch
            {
                0 => node,
                < 0 => GetNode(node.Left, key),
                _ => GetNode(node.Right, key)
            };
        }

        public override void Remove(TK key)
        {
            var node = GetNode(Root, key);
            if (node != null) Root = Remove(Root, key);
        }

        private Node Remove(Node node, TK key)
        {
            if (node == null)
                return null;

            Node retNode;
            switch (key.CompareTo(node.Key))
            {
                case < 0:
                    node.Left = Remove(node.Left, key);
                    retNode = node;
                    break;
                case > 0:
                    node.Right = Remove(node.Right, key);
                    retNode = node;
                    break;
                default:
                {
                    if (node.Left == null)
                    {
                        var rightNode = node.Right;
                        node.Right = null;
                        _size--;
                        retNode = rightNode;
                    }
                    else if (node.Right == null)
                    {
                        var leftNode = node.Left;
                        node.Left = null;
                        _size--;
                        retNode = leftNode;
                    }
                    else
                    {
                        var successor = Minimum(node.Right);
                        successor.Right = Remove(node.Right, successor.Key);
                        successor.Left = node.Left;
                        node.Left = node.Right = null;
                        retNode = successor;
                    }

                    break;
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
            if (balanceFactor >= -1 || GetBalanceFactor(retNode.Right) <= 0) return retNode;
            retNode.Right = RightRotate(retNode.Right);
            return LeftRotate(retNode);
        }


        private Node Minimum(Node node)
        {
            return node.Left == null ? node : Minimum(node.Left);
        }

        public override bool Contains(TK key)
        {
            return GetNode(Root, key) != null;
        }

        private sealed class Node
        {
            public Node(TK key, TV value)
            {
                Key = key;
                Value = value;
            }

            public TK Key { get; }
            public TV Value { get; set; }
            public Node Left { set; get; }
            public Node Right { set; get; }
            public int Height { set; get; } = 1;
        }
    }
}