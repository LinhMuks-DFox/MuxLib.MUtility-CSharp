using System;
using System.Collections.Generic;
using MuxLib.MUtility.Collections.Metas.ABClass;
using MuxLib.MUtility.Collections.Tree.Heap;

namespace MuxLib.MUtility.Collections.Queue
{
    public sealed class PriorityQueue<TE> : ABCQueue<TE>
        where TE : IComparable
    {
        private readonly MaxHeap<TE> _maxHeap;

        public PriorityQueue()
        {
            _maxHeap = new MaxHeap<TE>();
        }

        public override int Size => _maxHeap.Size;

        public override bool Empty => Size == 0;

        public override TE Dequeue()
        {
            return _maxHeap.ExtractMax();
        }

        public override void Enqueue(TE ele)
        {
            _maxHeap.Add(ele);
        }

        public override TE Peek()
        {
            return _maxHeap.PeekMax();
        }

        public override void Load(IEnumerable<TE> metaArray)
        {
            _maxHeap.Load(metaArray);
        }
    }
}