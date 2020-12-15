using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuxLib.MUtility.Collections.Table
{
    public sealed class MHashTable<K, V>
    {
        private class Entry
        {
            int Hash { set; get; } = -1;
            K Key { set; get; } = default;
            V Value { set; get; } = default;

            public Entry(int hash, K key, V value)
            {
                Hash = hash;
                Key = key;
                Value = value;
            }

            public Entry(int hash)
            {
                Hash = hash;
                Key = default;
                Value = default;
            }
        }
    }
}
