using System;
using System.Collections;
using System.Collections.Generic;
using MuxLib.MUtility.Collections.Errors;

namespace MuxLib.MUtility.Collections.List.LinkedList
{
    public sealed class LinkedList<T> : IList<T> /*Untested*/
    {
        private readonly Node _dummy_head;
        private readonly Node _tail;

        public LinkedList()
        {
            Count = 0;
            _dummy_head = new Node();
            _tail = new Node(null, _dummy_head);
        }

        public int Count { get; }

        public bool IsReadOnly { set; get; } = false;

        public T this[int index]
        {
            get => Get(index);
            set => Set(index, value);
        }

        public int IndexOf(T item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, T item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public void Add(T item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(T item)
        {
            var cur = _dummy_head;
            while (cur.Next != null)
            {
                if (cur.Data.Equals(item))
                    return true;
                cur = cur.Next;
            }

            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            var cur = _dummy_head;

            while (cur.Next != null)
            {
                if (cur.Before.Data.Equals(item))
                {
                    cur.Next.Before = cur.Before;
                    cur.Before.Next = cur.Next;
                    /* 1<->2<->3<->4 */
                    cur.Next = null;
                    cur.Before = null;
                    return true;
                }

                cur = cur.Next;
            }

            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (var i = 0; i < Count; i++) yield return Get(i);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for (var i = 0; i < Count; i++) yield return Get(i);
        }

        public T Get(int index)
        {
            if (index < 0 || index >= Count)
                throw new InvalidArgumentError(
                    $"Argument{index} is invalid.");

            var mid = Count / 2;
            if (index <= mid || index > 20)
            {
                var cur = _dummy_head;
                for (var i = 0; i < index + 1; i++) cur = cur.Next;

                return cur.Data;
            }
            else // index > mid;
            {
                var cur = _tail;
                for (var i = 0; i < Count - index + 1; i++) cur = cur.Before;

                return cur.Data;
            }
        }

        public void Set(int index, T item)
        {
            if (index < 0 || index >= Count)
                throw new InvalidArgumentError(
                    $"Argument{index} is invalid");

            var mid = Count / 2;
            if (index <= mid || index > 20)
            {
                var cur = _dummy_head;
                for (var i = 0; i < index + 1; i++) cur = cur.Next;

                cur.Data = item;
            }
            else // index > mid;
            {
                var cur = _tail;
                for (var i = 0; i < Count - index + 1; i++) cur = cur.Before;

                cur.Data = item;
            }
        }

        public void Swap(int j, int i)
        {
            var temp = this[i];
            this[i] = this[j];
            this[j] = temp;
        }

        private class Node
        {
            public Node()
            {
            }

            public Node(Node next, Node prev, T data)
            {
                (Next, Before, Data)
                    = (next, prev, data);
            }

            public Node(T data)
            {
                Data = data;
            }

            public Node(Node next, Node prev)
            {
                (Next, Before) = (next, prev);
            }

            public T Data { set; get; }

            public Node Next { set; get; }

            public Node Before { set; get; }
        }
    }
}