using System.Collections;
using System.Collections.Generic;

namespace MuxLib.MUtility.Collections.List.LinkedList
{
    public sealed class LinkedList<T> : IList<T>
    {

        private class Node
        {
            public T Data { set; get; } = default;

            public Node Next { set; get; } = null;

            public Node Before { set; get; } = null;

            public Node() { }

            public Node(Node next, Node prev, T data) => (Next, Before, Data)
                                                            = (next, prev, data);

            public Node(T data) { Data = data; }

            public Node(Node next, Node prev) => (Next, Before) = (next, prev);
        }
        private int _size;
        private Node _dummy_head;
        private Node _tail;

        public int Count { get => _size; }

        public bool IsReadOnly { set; get; } = false;

        public T this[int index]
        {
            get => Get(index);
            set => Set(index, value);
        }

        public LinkedList()
        {
            _size = 0; _dummy_head = new Node();
            _tail = new Node(null, _dummy_head);
        }

        public int IndexOf(T item)
        {
            throw new System.NotImplementedException();
        }

        public void Insert(int index, T item)
        {
            throw new System.NotImplementedException();
        }

        public T Get(int index)
        {
            if (index < 0 || index >= _size)
                throw new Errors.InvalidArgumentError(
                    $"Argument{index} is invalid.");

            int mid = _size / 2;
            if (index <= mid || index > 20)
            {
                Node cur = _dummy_head;
                for (int i = 0; i < index + 1; i++)
                {
                    cur = cur.Next;
                }

                return cur.Data;
            }
            else // index > mid;
            {
                Node cur = _tail;
                for (int i = 0; i < (_size - index) + 1; i++)
                {
                    cur = cur.Before;
                }

                return cur.Data;
            }
        }

        public void Set(int index, T item)
        {
            if (index < 0 || index >= _size)
                throw new Errors.InvalidArgumentError(
                    $"Argument{index} is invalid");

            int mid = _size / 2;
            if (index <= mid || index > 20)
            {
                Node cur = _dummy_head;
                for (int i = 0; i < index + 1; i++)
                {
                    cur = cur.Next;
                }

                cur.Data = item;
            }
            else // index > mid;
            {
                Node cur = _tail;
                for (int i = 0; i < (_size - index) + 1; i++)
                {
                    cur = cur.Before;
                }

                cur.Data = item;
            }
        }
        public void RemoveAt(int index)
        {
            throw new System.NotImplementedException();
        }

        public void Add(T item)
        {
            throw new System.NotImplementedException();
        }

        public void Clear()
        {
            throw new System.NotImplementedException();
        }

        public bool Contains(T item)
        {
            Node cur = _dummy_head;
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
            throw new System.NotImplementedException();
        }

        public bool Remove(T item)
        {
            Node cur = _dummy_head;

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
            for (int i = 0; i < _size; i++)
            {
                yield return Get(i);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for (int i = 0; i < _size; i++)
            {
                yield return Get(i);
            }
        }

        public void Swap(int j, int i)
        {
            T temp = this[i]; this[i] = this[j]; this[j] = temp;
        }
    }
}
