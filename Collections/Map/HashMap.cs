using MuxLib.MUtility.Collections.Metas.ABClass;
using MuxLib.MUtility.Collections.Table;

namespace MuxLib.MUtility.Collections.Map
{
    public sealed class HashMap<K, V> : ABCMap<K, V>
    {
        private readonly HashTable<K, V> _datas;

        public HashMap()
        {
            _datas = new HashTable<K, V>();
        }

        public override int Size => _datas.Size;

        public override bool Empty => Size == 0;

        public override void Add(K key, V value)
        {
            _datas.Append(key, value);
        }

        public override bool Contains(K key)
        {
            return _datas.Contains(key);
        }

        public override V Get(K key)
        {
            return _datas[key];
        }

        public override V Remove(K key)
        {
            return _datas.Remove(key);
        }

        public override void Set(K key, V newValue)
        {
            _datas[key] = newValue;
        }
    }
}