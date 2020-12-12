using System.Collections.Generic;
namespace MuxLib.MUtility.Collections.Table
{
    public sealed class HashTable<K, V>
    {
        private readonly int[] Capacities
        = {
            53,         97,         193,        389,        769,
            1543,       3079,       6151,       12289,      24593,
            49157,      98317,      196613,     393241,     786433,
            1572869,    3145739,    6291469,    12582917,   25165843,
            50331653,   100663319,  201326611,  402653189,  805306457,
            1610612741
        };
        private SortedDictionary<K, V>[] _hashTable;
        private int _M;
        private int _size;
        private const int UpperTol = 10, LowTol = 2;
        private int _capacityIndex = 0;
        public int Size { get => _size; }

        private void Resize(int new_m)
        {
            SortedDictionary<K, V>[] newHashTable =
                                            new SortedDictionary<K, V>[new_m];
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

        public HashTable()
        {
            _size = 0;
            _M = Capacities[_capacityIndex];
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

                if (_size >= UpperTol * _M &&
                    _capacityIndex + 1 < Capacities.Length)
                {
                    _capacityIndex++;
                    Resize(2 * _M);
                }
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
                if (_size < LowTol * _M && _capacityIndex - 1 >= 0)
                {
                    _capacityIndex--;
                    Resize(_capacityIndex);
                }
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