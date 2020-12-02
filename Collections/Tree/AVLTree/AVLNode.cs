using System;

namespace MuxLib.MUtility.Collections.Tree.AVLTree
{
    internal class AVLNode<K, V>
        where K : IComparable
    {
        public K Key { get; set; }
        public V Value { get; set; }
        public AVLNode<K, V> Left { set; get; } = null;
        public AVLNode<K, V> Right { set; get; } = null;
        public int Height { set; get; } = 1;

        public AVLNode(K key, V value)
        {
            Key = key; Value = value;
        }
    }
}
