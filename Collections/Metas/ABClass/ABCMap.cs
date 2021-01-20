namespace MuxLib.MUtility.Collections.Metas.ABClass
{
    public abstract class ABCMap<TK, TV>
    {
        public abstract int Size { get; }
        public abstract bool Empty { get; }
        public abstract void Add(TK key, TV value);
        public abstract TV Remove(TK key);
        public abstract bool Contains(TK key);
        public abstract TV Get(TK key);
        public abstract void Set(TK key, TV newValue);
    }
}