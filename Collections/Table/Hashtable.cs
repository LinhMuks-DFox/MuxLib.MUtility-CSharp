using System.Collections.Generic;
using MuxLib.MUtility.Collections.Errors;

namespace MuxLib.MUtility.Collections.Table
{
    public sealed class HashTable<K, V>
    {
        private const int UpperTol = 10, LowTol = 2;

        private readonly int[] Capacities
            =
            {
                53, 97, 193, 389, 769,
                1543, 3079, 6151, 12289, 24593,
                49157, 98317, 196613, 393241, 786433,
                1572869, 3145739, 6291469, 12582917, 25165843,
                50331653, 100663319, 201326611, 402653189, 805306457,
                1610612741
            };

        private int _capacityIndex;
        private SortedDictionary<K, V>[] _hashTable;
        private int _M;

        public HashTable()
        {
            Size = 0;
            _M = Capacities[_capacityIndex];
            _hashTable = new SortedDictionary<K, V>[_M];
            for (var i = 0; i < _M; ++i) _hashTable[i] = new SortedDictionary<K, V>();
        }

        public int Size { get; private set; }

        public V this[K key]
        {
            get => _hashTable[Hash(key)][key];
            set
            {
                var map = _hashTable[Hash(key)];
                if (map.ContainsKey(key)) throw new InvalidOperation($"{key} dose not exist");
                map[key] = value;
            }
        }

        private void Resize(int new_m)
        {
            var newHashTable =
                new SortedDictionary<K, V>[new_m];
            for (var i = 0; i < new_m; ++i) newHashTable[i] = new SortedDictionary<K, V>();
            var old_M = _M;
            _M = new_m;
            for (var i = 0; i < old_M; ++i)
            {
                var map = _hashTable[i];
                foreach (var key in map.Keys)
                    newHashTable[Hash(key)][key] = map[key];
            }

            _hashTable = newHashTable;
        }

        private int Hash(K key)
        {
            return (key.GetHashCode() & 0x7fffffff) % _M;
        }

        public void Append(K key, V value)
        {
            var map = _hashTable[Hash(key)];
            if (map.ContainsKey(key))
            {
                map[key] = value;
            }
            else
            {
                map.Add(key, value);
                Size++;

                if (Size >= UpperTol * _M && _capacityIndex + 1 < Capacities.Length)
                {
                    _capacityIndex++;
                    Resize(2 * _M);
                }
            }
        }

        public V Remove(K key)
        {
            var map = _hashTable[Hash(key)];
            V ret = default;
            if (map.ContainsKey(key))
            {
                ret = map[key];
                map.Remove(key);
                Size--;
                if (Size < LowTol * _M && _capacityIndex - 1 >= 0)
                {
                    _capacityIndex--;
                    Resize(_capacityIndex);
                }
            }

            return ret;
        }

        public bool Contains(K key)
        {
            var map = _hashTable[Hash(key)];
            return map.ContainsKey(key);
        }
    }
}