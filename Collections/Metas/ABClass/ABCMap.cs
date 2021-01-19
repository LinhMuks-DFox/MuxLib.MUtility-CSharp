namespace MuxLib.MUtility.Collections.Metas.ABClass
{
    public abstract class ABCMap<K, V>
    {
        public abstract int Size { get; }
        public abstract bool Empty { get; }
        public abstract void Add(K key, V value);
        public abstract V Remove(K key);
        public abstract bool Contains(K key);
        public abstract V Get(K key);
        public abstract void Set(K key, V newValue);
    }
}