using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuxLib.MUtility.Collections.Metas.ABClass
{
    public abstract class ABCMap<K, V>
    {
        public abstract void Add(K key, V value);
        public abstract V Remove(K key);
        public abstract bool Contains(K key);
        public abstract V Get(K key);
        public abstract void Set(K key, V newValue);
        public abstract int GetSize { get; }
        public abstract bool IsEmpty { get; }
    }
}
