using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuxLib.MUtility.Collections.Metas.ABClass
{
    public abstract class ABCSet<E>
    {

        public abstract void Add(E e);
        public abstract bool Contains(E e);
        public abstract void Remove(E e);
        public abstract int Size { get; }
        public abstract bool IsEmpty { get; }
    }
}
