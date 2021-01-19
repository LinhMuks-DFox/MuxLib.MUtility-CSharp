using MuxLib.MUtility.Collections.Errors;

namespace MuxLib.MUtility.Collections.Table
{
    internal class DicList<K, V>
    {
        private V[] _values;

        public DicList(int capacity)
        {
            Keys = new K[capacity];
            _values = new V[capacity];
            Size = 0;
        }

        public DicList()
        {
            Keys = new K[1000];
            _values = new V[1000];
            Size = 0;
        }

        public int Size { get; private set; }

        public K[] Keys { get; private set; }


        public V this[K key]
        {
            get
            {
                var index = IndexOf(key);
                if (index == -1)
                    throw new InvalidArgumentError($"{key}is not exist.");
                return _values[index];
            }
            set
            {
                var index = IndexOf(key);
                if (index == -1) Append(key, value);
                _values[index] = value;
            }
        }

        public void Resize(int new_capacity)
        {
            var new_keys = new K[new_capacity];
            var new_valu = new V[new_capacity];
            for (var i = 0; i < Size; ++i)
            {
                new_valu[i] = _values[i];
                new_keys[i] = Keys[i];
            }

            Keys = new_keys;
            _values = new_valu;
        }

        private int IndexOf(K key)
        {
            for (var i = 0; i < Size; ++i)
                if (Keys[i].Equals(key))
                    return i;
            return -1;
        }

        public bool ContainsKey(K key)
        {
            return IndexOf(key) != -1;
        }

        private void Append(K key, V value)
        {
            if (Size.Equals(Keys.Length))
                Resize(2 * Keys.Length);
            Keys[Size] = key;
            _values[Size] = value;
            Size++;
        }

        public void Remove(K key)
        {
            var index = IndexOf(key);
            if (index == -1)
                throw new InvalidArgumentError($"Remove failed. {key} is not exist.");
            for (var i = index + 1; i < Size; i++)
            {
                Keys[i - 1] = Keys[i];
                _values[i - 1] = _values[i];
            }

            Size--;
            if (Size == Keys.Length / 4 && Keys.Length / 2 != 0) Resize(Keys.Length / 2);
        }
    }

    public class ReHashTable<K, V>
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
        private DicList<K, V>[] _hashTable;
        private int _M;

        public ReHashTable()
        {
            Size = 0;
            _M = Capacities[_capacityIndex];
            _hashTable = new DicList<K, V>[_M];
            for (var i = 0; i < _M; ++i) _hashTable[i] = new DicList<K, V>();
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
            var newHashTable = new DicList<K, V>[new_m];
            for (var i = 0; i < new_m; ++i) newHashTable[i] = new DicList<K, V>();
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
                map[key] = value;
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