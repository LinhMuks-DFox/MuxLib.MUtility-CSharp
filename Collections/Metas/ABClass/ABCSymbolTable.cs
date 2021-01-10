namespace MuxLib.MUtility.Collections.Metas.ABClass
{
    public abstract class ABCSymbolTable<K, V>
    {
        public abstract void Set(K key, V value);

        public abstract V Get(K key);

        public abstract V Remove(K key);

        public abstract bool Contains(K key);

        public abstract bool Empty { get; }

        public abstract int Size { get; }

        public abstract V this[K key] { set; get; }
    }
}
