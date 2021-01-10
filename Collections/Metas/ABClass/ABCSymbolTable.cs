namespace MuxLib.MUtility.Collections.Metas.ABClass
{
    public abstract class ABCSymbolTable<K, V>
    {

        public abstract void Remove(K key);

        public abstract bool Contains(K key);

        public abstract bool Empty { get; }

        public abstract int Size { get; }

        public abstract V this[K key] { set; get; }
    }
}
