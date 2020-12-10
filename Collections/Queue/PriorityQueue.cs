using System;
using System.Collections.Generic;
using MuxLib.MUtility.Collections.Metas.ABClass;
using MuxLib.MUtility.Collections.Tree.Heap;
namespace MuxLib.MUtility.Collections.Queue
{
    public sealed class PriorityQueue<E> : ABCQueue<E>
        where E : IComparable
    {
        public override int Size { get => _maxHeap.Size; }

        public override bool Empty { get => Size == 0; }

        private MaxHeap<E> _maxHeap;

        public PriorityQueue()
        {
            _maxHeap = new MaxHeap<E>();

        }
        public override E Dequeue()
        {
            return _maxHeap.ExtractMax();
        }

        public override void Enqueue(E ele)
        {
            _maxHeap.Add(ele);
        }

        public override E Peek()
        {
            return _maxHeap.PeekMax();
        }

        public override void Load(IEnumerable<E> meta_array)
        {
            _maxHeap.Load(meta_array);
        }
    }
}
