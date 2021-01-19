using System;
using MuxLib.MUtility.Algorithms.Metas;
using MuxLib.MUtility.Collections.Tree.Heap;

namespace MuxLib.MUtility.Algorithms.Sorter
{
    public sealed class HeapSorter<T> : Sorter<T>
        where T : IComparable
    {
        public override void Sort(T[] arr)
        {
            var max = new MaxHeap<T>(arr);
            for (var i = 0; i < arr.Length; ++i) arr[i] = max.ExtractMax();
        }
    }
}