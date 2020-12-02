using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if (Math.Abs(balance_factor) > 1)
                Console.WriteLine("Unbalance: ", balance_factor);
            return node;

        }

        private static int GetBalanceFactor(AVLNode<K,V> node)
        {
            if (node == null)
                return 0;
            return GetHeight(node.Left) - GetHeight(node.Right);
        }

        public bool IsBST()
        {
            List<K> keys = new List<K>();
            InOrder(_root, keys);
            for(int i = 1; i < keys.Count; i++)
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

        private bool IsBalanced(AVLNode<K,V> node)
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
    }
}
