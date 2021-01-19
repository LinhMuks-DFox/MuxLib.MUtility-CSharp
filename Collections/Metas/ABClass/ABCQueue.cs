using System.Collections.Generic;

namespace MuxLib.MUtility.Collections.Metas.ABClass
{
    public abstract class ABCQueue<TData>
    {
        public abstract int Size { get; }

        public abstract bool Empty { get; }
        public abstract void Enqueue(TData ele);

        public abstract TData Dequeue();

        public abstract TData Peek();

        public abstract void Load(IEnumerable<TData> meta_array);
    }
}