using System.Collections.Generic;

namespace MuxLib.MUtility.Collections.Metas.ABClass
{
    public abstract class ABCQueue<TData>
    {

        abstract public int Size { get; }

        abstract public bool Empty { get; }
        abstract public void Enqueue(TData ele);

        abstract public TData Dequeue();

        abstract public TData Peek();

        abstract public void Load(IEnumerable<TData> meta_array);
    }
}
