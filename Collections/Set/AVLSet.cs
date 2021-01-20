using System;
using MuxLib.MUtility.Collections.Metas.ABClass;
using MuxLib.MUtility.Collections.Tree.AVLTree;

namespace MuxLib.MUtility.Collections.Set
{
    public sealed class AvlSet<TE>
        : ABCSet<TE>
        where TE : IComparable
    {
        private readonly AvlTree<TE, object> _data;

        public AvlSet()
        {
            _data = new AvlTree<TE, object>();
        }

        public override int Size => _data.Size;

        public override bool Empty => _data.Empty;

        public override void Add(TE e)
        {
            _data.Append(e, null);
        }

        public override bool Contains(TE e)
        {
            return _data.Contains(e);
        }

        public override void Remove(TE e)
        {
            _data.Remove(e);
        }
    }
}