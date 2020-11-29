using System.Collections;
using System.Collections.Generic;

namespace MuxLib.MUtility.Collections.List.LinkedList
{
    public class LinkedList<T> : IList<T>
    {
        private int _size;
        private Node<T> _dummy_head;
        private Node<T> _tail;

        public int Count { get => _size; }

        public bool IsReadOnly { set; get; } = false;

        public T this[int index] { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public LinkedList()
        {
            _size = 0; _dummy_head = new Node<T>();
            _tail = new Node<T>(null, _dummy_head);
        }

        public int IndexOf(T item)
        {
            throw new System.NotImplementedException();
        }

        public void Insert(int index, T item)
        {
            throw new System.NotImplementedException();
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
            throw new System.NotImplementedException();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new System.NotImplementedException();
        }

        public bool Remove(T item)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new System.NotImplementedException();
        }
    }
}
