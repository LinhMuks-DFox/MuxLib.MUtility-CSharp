namespace MuxLib.MUtility.Collections.Table
{
    internal class DicList<K, V>
    {
        private K[] _keys;
        private V[] _values;
        private int _size;
        public int Size { get => _size; }

        public K[] Keys { get => _keys; }

        public void Resize(int new_capacity)
        {
            K[] new_keys = new K[new_capacity];
            V[] new_valu = new V[new_capacity];
            for (int i = 0; i < Size; ++i)
            {
                new_valu[i] = _values[i]; new_keys[i] = _keys[i];
            }

            _keys = new_keys; _values = new_valu;
        }

        public DicList(int capacity)
        {
            _keys = new K[capacity];
            _values = new V[capacity];
            _size = 0;
        }

        public DicList()
        {
            _keys = new K[1000];
            _values = new V[1000];
            _size = 0;
        }


        public V this[K key]
        {
            get
            {
                int index = IndexOf(key);
                if (index == -1)
                    throw new Errors.InvalidArgumentError($"{key}is not exist.");
                return _values[index];
            }
            set
            {
                int index = IndexOf(key);
                if (index == -1)
                {
                    Append(key, value);
                }
                _values[index] = value;
            }
        }

        private int IndexOf(K key)
        {
            for (int i = 0; i < _size; ++i)
                if (_keys[i].Equals(key))
                    return i;
            return -1;
        }

        public bool ContainsKey(K key)
        {
            return IndexOf(key) == -1;
        }

        private void Append(K key, V value)
        {
            if (_size.Equals(_keys.Length))
                Resize(2 * _keys.Length);
            _keys[_size] = key;
            _values[_size] = value; _size++;
        }

        public void Remove(K key)
        {
            int index = IndexOf(key);
            if (index == -1)
                throw new Errors.InvalidArgumentError($"Remove failed. {key} is not exist.");
            for (int i = index + 1; i < _size; i++)
            {
                _keys[i - 1] = _keys[i];
                _values[i - 1] = _values[i];
            }
            _size--;
            if (_size == _keys.Length / 4 && _keys.Length / 2 != 0)
            {
                Resize(_keys.Length / 2);
            }
        }
    }
    public class ReHashTable<K, V>
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
        private DicList<K, V>[] _hashTable;
        private int _M;
        private int _size;
        private const int UpperTol = 10, LowTol = 2;
        private int _capacityIndex = 0;
        public int Size { get => _size; }

        private void Resize(int new_m)
        {
            DicList<K, V>[] newHashTable = new DicList<K, V>[new_m];
            for (int i = 0; i < new_m; ++i)
            {
                newHashTable[i] = new DicList<K, V>();
            }
            int old_M = _M;
            _M = new_m;
            for (int i = 0; i < old_M; ++i)
            {
                DicList<K, V> map = _hashTable[i];
                foreach (K key in map.Keys)
                    newHashTable[Hash(key)][key] = map[key];
            }

            _hashTable = newHashTable;
        }

        public ReHashTable()
        {
            _size = 0;
            _M = Capacities[_capacityIndex];
            _hashTable = new DicList<K, V>[_M];
            for (int i = 0; i < _M; ++i) _hashTable[i] = new DicList<K, V>();
        }

        public V this[K key]
        {
            get => _hashTable[Hash(key)][key];
            set
            {
                DicList<K, V> map = _hashTable[Hash(key)];
                if (map.ContainsKey(key)) throw new Errors.InvalidOperation($"{key} dose not exist");
                map[key] = value;
            }
        }

        private int Hash(K key)
        {
            return (key.GetHashCode() & 0x7fffffff) % _M;
        }

        public void Append(K key, V value)
        {
            DicList<K, V> map = _hashTable[Hash(key)];
            if (map.ContainsKey(key))
                map[key] = value;
            else
            {
                map[key] = value;
                _size++;

                if (_size >= UpperTol * _M && _capacityIndex + 1 < Capacities.Length)
                {
                    _capacityIndex++;
                    Resize(2 * _M);
                }
            }
        }

        public V Remove(K key)
        {
            DicList<K, V> map = _hashTable[Hash(key)];
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
            DicList<K, V> map = _hashTable[Hash(key)];
            return map.ContainsKey(key);
        }
    }
}
