using MuxLib.MUtility.Collections.Metas.ABClass;
using MuxLib.MUtility.Collections.Table;

namespace MuxLib.MUtility.Collections.Map
{
    public sealed class HashMap<TK, TV> : ABCMap<TK, TV>
    {
        private readonly HashTable<TK, TV> _data;

        public HashMap()
        {
            _data = new HashTable<TK, TV>();
        }

        public override int Size => _data.Size;

        public override bool Empty => Size == 0;

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
            return _data.Remove(key);
        }

        public override void Set(TK key, TV newValue)
        {
            _data[key] = newValue;
        }
    }
}