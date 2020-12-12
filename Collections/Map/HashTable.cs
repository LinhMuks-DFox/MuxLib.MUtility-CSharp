using System.Collections.Generic;
namespace MuxLib.MUtility.Collections.Map
{
    public sealed class HashTable<K, V>
    {
        private SortedDictionary<K, V>[] _hashTable;
        private int _M;
        private int _size;
        private const int upperTol = 10, lowTol = 2, initCapacity = 7;
        public int Size { get => _size; }

        private void Resize(int new_m)
        {
            SortedDictionary<K, V>[] newHashTable = new SortedDictionary<K, V>[new_m];
            for (int i = 0; i < new_m; ++i)
            {
                newHashTable[i] = new SortedDictionary<K, V>();
            }
            int old_M = _M;
            _M = new_m;
            for (int i = 0; i < old_M; ++i)
            {
                SortedDictionary<K, V> map = _hashTable[i];
                foreach (K key in map.Keys)
                    newHashTable[Hash(key)][key] = map[key];
            }

            _hashTable = newHashTable;
        }

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
            _M = initCapacity;
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

                if (_size >= upperTol * _M)
                    Resize(2 * _M);
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
                if (_size < lowTol * _M && _M / 2 >= initCapacity)
                    Resize(_M / 2);
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