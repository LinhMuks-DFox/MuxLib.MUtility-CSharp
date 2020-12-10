using System;
using MuxLib.MUtility.Collections.Metas.ABClass;
using MuxLib.MUtility.Collections.Tree.AVLTree;
namespace MuxLib.MUtility.Collections.Set
{
    public sealed class AVLSet<E>
        : ABCSet<E>
        where E : IComparable
    {
        private AVLTree<E, object> _avl;

        public AVLSet()
        {
            _avl = new AVLTree<E, object>();
        }
        public override int Size => _avl.Size;

        public override bool Empty => _avl.IsEmpty;

        public override void Add(E e)
        {
            _avl.Append(e, null);
        }

        public override bool Contains(E e)
        {
            return _avl.Contains(e);
        }

        public override void Remove(E e)
        {
            _avl.Remove(e);
        }
    }
}
