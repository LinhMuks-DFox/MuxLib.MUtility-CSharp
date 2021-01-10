using System;
using MuxLib.MUtility.Collections.Metas.ABClass;
using MuxLib.MUtility.Collections.Tree.AVLTree;
namespace MuxLib.MUtility.Collections.Set
{
    public sealed class AVLSet<E>
        : ABCSet<E>
        where E : IComparable
    {
        private readonly AVLTree<E, object> _data;

        public AVLSet()
        {
            _data = new AVLTree<E, object>();
        }
        public override int Size => _data.Size;

        public override bool Empty => _data.Empty;

        public override void Add(E e)
        {
            _data.Append(e, null);
        }

        public override bool Contains(E e)
        {
            return _data.Contains(e);
        }

        public override void Remove(E e)
        {
            _data.Remove(e);
        }
    }
}
