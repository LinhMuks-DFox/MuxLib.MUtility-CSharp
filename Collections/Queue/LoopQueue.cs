using System.Collections.Generic;
using System.Text;
using MuxLib.MUtility.Collections.Errors;
using MuxLib.MUtility.Collections.Metas.ABClass;

namespace MuxLib.MUtility.Collections.Queue
{
    public sealed class LoopQueue<T> : ABCQueue<T>
    {
        private T[] _data;
        private int _front, _tail;
        private int _size;

        public LoopQueue(int capacity)
        {
            _data = new T[capacity + 1];

            _front = _tail = _size = 0;
        }

        public LoopQueue()
        {
            _data = new T[11];
            _front = _tail = _size = 0;
        }

        public int Capacity => _data.Length - 1;
        public override bool Empty => _front == _tail;
        public override int Size => _size;


        public override T Dequeue()
        {
            if (Empty) throw new InvalidArgumentError("Can not dequeue from an empty queue");
            var ret = _data[_front];
            // GC.Collect(0, GCCollectionMode.Default);
            _front = (_front + 1) % _data.Length;
            _size--;
            if (_size == Capacity / 4 && Capacity / 2 != 0) Resize(Capacity / 2);
            return ret;
        }

        public override void Enqueue(T ele)
        {
            if ((_tail + 1) % _data.Length == _front)
                Resize(Capacity * 2);
            _data[_tail] = ele;
            _tail = (_tail + 1) % _data.Length;
            _size++;
        }

        public override T Peek()
        {
            if (Empty) throw new InvalidArgumentError("Can not get_front from an empty queue");
            return _data[_front];
        }

        private void Resize(int newCapacity)
        {
            var newData = new T[newCapacity + 1];
            for (var i = 0; i < _size; i++)
                newData[i] = _data[i + _front % _data.Length];
            _data = newData;
            _front = 0;
            _tail = _size;
        }

        public override string ToString()
        {
            var res = new StringBuilder();
            res.Append($"LoopQueue<{typeof(T)}> Object: size: {Size} ");
            res.Append("Front { ");
            for (var i = _front; i != _tail; i = (i + 1) % _data.Length)
            {
                res.Append(_data[i]);
                if ((i + 1) % _data.Length != _tail)
                    res.Append(", ");
            }

            res.Append(" } Tail");
            return res.ToString();
        }

        public override void Load(IEnumerable<T> metaArray)
        {
            foreach (var e in metaArray)
                Enqueue(e);
        }
    }
}