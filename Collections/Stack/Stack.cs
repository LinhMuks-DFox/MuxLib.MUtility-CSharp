using System.Collections.Generic;
using MuxLib.MUtility.Collections.Metas.ABClass;
using MuxLib.MUtility.Collections.List.ArrayList;
using System.Text;

namespace MuxLib.MUtility.Collections.Stack
{
    public sealed class Stack<T>
        : ABCStack<T>
    {
        private readonly ArrayList<T> _array;
        public override bool Empty => _array.Count == 0;
        public override int Size => _array.Count;

        public Stack(int capacity) => _array = new ArrayList<T>(capacity);


        public Stack() => _array = new ArrayList<T>();

        public Stack(T[] arr) => _array = new ArrayList<T>(arr);


        public override T Peek() => _array.GetLast();

        public override T Pop() => _array.RemoveLast();

        public override void Push(T e) => _array.Add(e);

        public override void Load(IEnumerable<T> metaArray)
        {
            foreach (T e in metaArray)
            {
                Push(e);
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"DynamicArray_Based Stack<{typeof(T)}> object");
            sb.Append("Bottom:{ ");
            for (int i = 0; i < Size; i++)
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
