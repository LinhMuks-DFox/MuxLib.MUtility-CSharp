using System;
using MuxLib.MUtility.Collections.Metas.ABClass;
using MuxLib.MUtility.Collections.Tree.AVLTree;
namespace MuxLib.MUtility.Collections.Map
{
    public sealed class AVLMap<K, V>
        : ABCMap<K, V>
        where K : IComparable
    {
        private readonly AVLTree<K, V> _data;

        public AVLMap()
        {
            _data = new AVLTree<K, V>();
        }
        public override int Size => _data.Size;

        public override bool Empty => _data.IsEmpty;

        public override void Add(K key, V value)
        {
            _data.Append(key, value);
        }

        public override bool Contains(K key)
        {
            return _data.Contains(key);
        }

        public override V Get(K key)
        {
            return _data[key];
        }

        public override V Remove(K key)
        {
            return _data.Remove(key);
        }

        public override void Set(K key, V new_value)
        {
            _data[key] = new_value;
        }
    }
}
