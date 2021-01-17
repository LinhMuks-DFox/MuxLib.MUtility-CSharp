using System.Collections.Generic;
using MuxLib.MUtility.Com.Attr;
namespace MuxLib.MUtility.Collections.Tree.AVLTree
{
    [Unimplemented]
    public class R_AVLTree<K, V> : Metas.ABClass.ABCOrderSymbolTable<K, V>
        where K : System.IComparable
    {
        public override void Remove(K key)
        {
            throw new System.NotImplementedException();
        }

        public override bool Contains(K key)
        {
            throw new System.NotImplementedException();
        }

        public override bool Empty => Size == 0;
        public override int Size { get; }

        public override V this[K key]
        {
            get => throw new System.NotImplementedException();
            set => throw new System.NotImplementedException();
        }

        public override K Min()
        {
            throw new System.NotImplementedException();
        }

        public override K Max()
        {
            throw new System.NotImplementedException();
        }

        public override K Floor(K key)
        {
            throw new System.NotImplementedException();
        }

        public override K Ceiling(K key)
        {
            throw new System.NotImplementedException();
        }

        public override int Rank(K key)
        {
            throw new System.NotImplementedException();
        }

        public override K Select(int k)
        {
            throw new System.NotImplementedException();
        }

        public override void DeleteMin()
        {
            throw new System.NotImplementedException();
        }

        public override void DeleteMax()
        {
            throw new System.NotImplementedException();
        }

        public override int NumberOf(K low, K max)
        {
            throw new System.NotImplementedException();
        }

        public override IEnumerable<K> SortedKeys(K low, K max)
        {
            throw new System.NotImplementedException();
        }
    }
}
