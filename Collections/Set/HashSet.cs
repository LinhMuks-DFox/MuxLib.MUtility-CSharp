using MuxLib.MUtility.Collections.Metas.ABClass;
using MuxLib.MUtility.Collections.Table;

namespace MuxLib.MUtility.Collections.Set
{
    public sealed class HashSet<T> : ABCSet<T>
    {
        private readonly HashTable<T, object> _data;

        public HashSet()
        {
            _data = new HashTable<T, object>();
        }

        public override int Size => _data.Size;

        public override bool Empty => Size == 0;

        public override void Add(T e)
        {
            _data.Append(e, null);
        }

        public override bool Contains(T e)
        {
            return _data.Contains(e);
        }

        public override void Remove(T e)
        {
            _data.Remove(e);
        }
    }
}