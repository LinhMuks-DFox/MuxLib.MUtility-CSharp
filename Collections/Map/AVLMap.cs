using System;
using MuxLib.MUtility.Collections.Metas.ABClass;
using MuxLib.MUtility.Collections.Tree.AVLTree;

namespace MuxLib.MUtility.Collections.Map
{
    public sealed class AvlMap<TK, TV>
        : ABCMap<TK, TV>
        where TK : IComparable
    {
        private readonly AvlTree<TK, TV> _data;

        public AvlMap()
        {
            _data = new AvlTree<TK, TV>();
        }

        public override int Size => _data.Size;

        public override bool Empty => _data.Empty;

        public override void Add(TK key, TV value)
        {
            _data.Append(key, value);
        }

        public override bool Contains(TK key)
        {
            return _data.Contains(key);
        }

        public override TV Get(TK key)
        {
            return _data[key];
        }

        public override TV Remove(TK key)
        {
            var ret = _data[key];
            _data.Remove(key);
            return ret;
        }

        public override void Set(TK key, TV newValue)
        {
            _data[key] = newValue;
        }
    }
}