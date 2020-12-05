using System;
using System.Collections.Generic;

namespace MuxLib.MUtility.Collections.Tree.AVLTree
{
    public class AVLTree<K, V>
        where K : IComparable
    {
        private AVLNode<K, V> _root;
        private int _size;

        public int Size { get => _size; }
        public bool IsEmpty { get => Size == 0; }

        public AVLTree()
        {
            _root = null;
            _size = 0;
        }

        private static int GetHeight(AVLNode<K, V> node)
        {
            if (node == null)
                return 0;
            return node.Height;
        }

        public void Append(K key, V value)
        {
            Append(_root, key, value);
        }

        private AVLNode<K, V> Append(AVLNode<K, V> node, K key, V value)
        {
            if (node == null)
            {
                _size++;
                return new AVLNode<K, V>(key, value);
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
            if(balance_factor > 1 &&GetBalanceFactor(node.Left) < 0)
            {
                node.Left =  LeftRotate(node.Left); // Convert to LL
                return RightRotate(node);
            }

            // RL
            if(balance_factor < -1 && GetBalanceFactor(node.Right) > 0)
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
        private AVLNode<K, V> RightRotate(AVLNode<K, V> y)
        {
            AVLNode<K, V> x = y.Left, t3 = x.Right;
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
        private AVLNode<K, V> LeftRotate(AVLNode<K, V> y)
        {
            AVLNode<K, V> x = y.Right, T2 = x.Left;
            x.Left = y; y.Right = T2;
            y.Height = Math.Max(GetHeight(y.Left), GetHeight(y.Right)) + 1;
            x.Height = Math.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;
            return x;
        }


        private static int GetBalanceFactor(AVLNode<K, V> node)
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

        private bool IsBalanced(AVLNode<K, V> node)
        {
            if (node == null)
                return true;
            int balanced_factor = GetBalanceFactor(node);
            if (Math.Abs(balanced_factor) > 1)
                return false;
            return IsBalanced(node.Left) && IsBalanced(node.Right);
        }

        private void InOrder(AVLNode<K, V> node, List<K> keys)
        {
            if (node == null)
                return;
            InOrder(node.Left, keys);
            keys.Add(node.Key);
            InOrder(node.Right, keys);
        }

        public void Set(K key, V value)
        {
            AVLNode<K, V> node = GetNode(_root, key);
            if (node == null)
                throw new Errors.InvalidArgumentError($"{key} dose not exist");
            node.Value = value;
        }

        public V Get(K key)
        {
            AVLNode<K, V> node = GetNode(_root, key);
            if (node == null)
                throw new Errors.InvalidArgumentError($"{key} dose not exist");
            return node.Value;
        }

        private AVLNode<K, V> GetNode(AVLNode<K, V> node, K key)
        {
            if (node == null)
                return null;
            if (key.Equals(key))
                return node;
            else if (key.CompareTo(node.Key) < 0)
                return GetNode(node.Left, key);
            else
                return GetNode(node.Right, key);
        }
    }
}
