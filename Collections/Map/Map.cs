using System;
using MuxLib.MUtility.Collections.Metas.ABClass;
using MuxLib.MUtility.Collections.Tree.AVLTree;
namespace MuxLib.MUtility.Collections.Map
{
    public class Map<K, V>
        : ABCMap<K, V>
        where K : IComparable
    {
        private AVLTree<K, V> _avl;

        public Map()
        {
            _avl = new AVLTree<K, V>();
        }
        public override int Size => _avl.Size;

        public override bool Empty => _avl.IsEmpty;

        public override void Add(K key, V value)
        {
            _avl.Append(key, value);
        }

        public override bool Contains(K key)
        {
            return _avl.Contains(key);
        }

        public override V Get(K key)
        {
            return _avl[key];
        }

        public override V Remove(K key)
        {
            return _avl.Remove(key);
        }

        public override void Set(K key, V new_value)
        {
            _avl[key] = new_value;
        }
    }
}
