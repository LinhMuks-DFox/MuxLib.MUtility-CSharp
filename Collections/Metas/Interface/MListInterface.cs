using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuxLib.MUtility.Collections.Metas.Interface
{
    public interface MListInterface<TData>
    {
        public TData this[int index] { get; }

        public int Size { get; }
        public bool IsEmpty { get; }
    }
}
