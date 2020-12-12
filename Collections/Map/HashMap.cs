using MuxLib.MUtility.Collections.Table;
namespace MuxLib.MUtility.Collections.Map
{
    public sealed class HashMap<K, V> : Metas.ABClass.ABCMap<K, V>
    {
        private readonly HashTable<K, V> _datas;

        public override int Size => _datas.Size;

        public override bool Empty => Size == 0;

        public HashMap()
        {
            _datas = new HashTable<K, V>();
        }

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

        public override void Set(K key, V new_value)
        {
            _datas[key] = new_value;
        }
    }
}
