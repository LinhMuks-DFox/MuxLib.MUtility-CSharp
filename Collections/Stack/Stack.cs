using System.Collections.Generic;
using System.Text;
using MuxLib.MUtility.Collections.List.ArrayList;
using MuxLib.MUtility.Collections.Metas.ABClass;

namespace MuxLib.MUtility.Collections.Stack
{
    public sealed class Stack<T>
        : ABCStack<T>
    {
        private readonly ArrayList<T> _array;

        public Stack(int capacity)
        {
            _array = new ArrayList<T>(capacity);
        }


        public Stack()
        {
            _array = new ArrayList<T>();
        }

        public Stack(T[] arr)
        {
            _array = new ArrayList<T>(arr);
        }

        public override bool Empty => _array.Count == 0;
        public override int Size => _array.Count;


        public override T Peek()
        {
            return _array.GetLast();
        }

        public override T Pop()
        {
            return _array.RemoveLast();
        }

        public override void Push(T e)
        {
            _array.Add(e);
        }

        public override void Load(IEnumerable<T> metaArray)
        {
            foreach (var e in metaArray) Push(e);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"DynamicArray_Based Stack<{typeof(T)}> object");
            sb.Append("Bottom:{ ");
            for (var i = 0; i < Size; i++)
            {
                sb.Append(_array.Get(i) + "");
                if (i < Size - 1)
                    sb.Append(", ");
            }

            sb.Append(" }Top");
            return sb.ToString();
        }
    }
}