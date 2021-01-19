using System;
using System.Collections.Generic;
using MuxLib.MUtility.Collections.Metas.ABClass;
using MuxLib.MUtility.Collections.Tree.Heap;

namespace MuxLib.MUtility.Collections.Queue
{
    public sealed class PriorityQueue<E> : ABCQueue<E>
        where E : IComparable
    {
        private readonly MaxHeap<E> _maxHeap;

        public PriorityQueue()
        {
            _maxHeap = new MaxHeap<E>();
        }

        public override int Size => _maxHeap.Size;

        public override bool Empty => Size == 0;

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

        public override void Load(IEnumerable<E> metaArray)
        {
            _maxHeap.Load(metaArray);
        }
    }
}