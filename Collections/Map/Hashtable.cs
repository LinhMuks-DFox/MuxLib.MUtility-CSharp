using System.Collections.Generic;
namespace MuxLib.MUtility.Collections.Map
{
    public sealed class HashTable<K, V>
    {
        private readonly SortedDictionary<K, V>[] _hashTable;
        private readonly int _M;
        private int _size;

        public int Size { get => _size; }
        public HashTable(int M)
        {
            _size = 0;
            _M = M;
            _hashTable = new SortedDictionary<K, V>[M];
            for (int i = 0; i < M; ++i)
                _hashTable[i] = new SortedDictionary<K, V>();
        }

        public HashTable()
        {
            _size = 0;
            _M = 97;
            _hashTable = new SortedDictionary<K, V>[_M];
            for (int i = 0; i < _M; ++i)
                _hashTable[i] = new SortedDictionary<K, V>();
        }

        public V this[K key]
        {
            get
            {
                return _hashTable[Hash(key)][key];
            }

            set
            {
                SortedDictionary<K, V> map = _hashTable[Hash(key)];
                if (map.ContainsKey(key))
                    throw new Errors.InvalidOperation($"{key} dose not exist");
                map[key] = value;
            }
        }

        private int Hash(K key)
        {
            return (key.GetHashCode() & 0x7fffffff) % _M;
        }

        public void Append(K key, V value)
        {
            SortedDictionary<K, V> map = _hashTable[Hash(key)];
            if (map.ContainsKey(key))
                map[key] = value;
            else
            {
                map.Add(key, value);
                _size++;
            }
        }

        public V Remove(K key)
        {
            SortedDictionary<K, V> map = _hashTable[Hash(key)];
            V ret = default;
            if (map.ContainsKey(key))
            {
                ret = map[key];
                map.Remove(key);
                _size--;
            }
            return ret;
        }

        public bool Contains(K key)
        {
            SortedDictionary<K, V> map = _hashTable[Hash(key)];
            return map.ContainsKey(key);
        }
    }
}