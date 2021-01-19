using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using MuxLib.MUtility.Collections.Errors;

namespace MuxLib.MUtility.Collections.List.ArrayList
{
    public sealed class ArrayList<T> : IList<T>, ICollection<T>
    {
        public delegate int Compare(T item1, T item2);

        public ArrayList()
        {
            Count = 0;
            Data = new T[500];
        }

        public ArrayList(int capacity)
        {
            Count = 0;
            Data = new T[capacity];
        }

        public ArrayList(T[] data)
        {
            Count = 0;
            Data = new T[data.Length];
            for (var i = 0; i < data.Length; i++) Data[i] = data[i];
        }

        private T[] Data { set; get; }
        public bool IsEmpty => Count == 0;
        public int Count { private set; get; }


        public bool IsReadOnly { set; get; } = false;

        public T this[int index]
        {
            get => Get(index);
            set
            {
                if (index == Count) Insert(Count, value);
                else Set(index, value);
            }
        }

        public int IndexOf(T item)
        {
            for (int i = 0, j = Count - 1; i != j; i++, j--) // faster
            {
                if (Data[i].Equals(item))
                    return i;
                if (Data[j].Equals(item))
                    return j;
            }

            return -1;
        }

        public void Insert(int index, T item)
        {
            if (IsReadOnly)
                throw new InvalidOperation("Can not insert item into a ReadOnly ArrayList");
            if (Count.Equals(Data.Length))
                Resize(2 * Data.Length);

            if (index < 0 || index > Count)
                throw new InvalidArgumentError
                ("Insert failed. Invalid Index was passed." +
                 "Index should >= 0 and <= size");
            for (long i = Count - 1; i >= index; i--)
                Data[i + 1] = Data[i];
            Data[index] = item;
            Count++;
        }

        public void RemoveAt(int index)
        {
            if (IsReadOnly)
                throw new InvalidOperation("Can not Remove item into a ReadOnly ArrayList");
            if (index < 0 || index >= Count)
                throw new InvalidArgumentError("Remove failed. Index is invalid.");
            for (var i = index + 1; i < Count; i++)
                Data[i - 1] = Data[i];
            Count--;
            CheckResize();
        }

        public void Add(T item)
        {
            if (IsReadOnly)
                throw new InvalidOperation("Can not Add item into a ReadOnly ArrayList");
            Insert(Count, item);
        }

        public void Clear()
        {
            if (IsReadOnly)
                throw new InvalidOperation("Can not Clear item into a ReadOnly ArrayList");
            Count = 0;
            Data = new T[Data.Length];
        }

        public bool Contains(T item)
        {
            return IndexOf(item) != -1;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (arrayIndex < 0 || arrayIndex >= Count)
                throw new InvalidArgumentError($"Argument:{arrayIndex} is invalid");
            for (var i = 0; i < arrayIndex; i++)
                array[i] = Data[i];
        }

        public bool Remove(T item)
        {
            if (IsReadOnly)
                throw new InvalidOperation("Can not Remove item into a ReadOnly ArrayList");
            var index = IndexOf(item);
            if (index == -1) return false;
            Remove(index);
            return true;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (var i = 0; i < Count; i++) yield return Get(i);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for (var i = 0; i < Count; i++) yield return Get(i);
        }

        private void Set(int index, T item)
        {
            if (IsReadOnly)
                throw new InvalidOperation("Can not set item into a ReadOnly ArrayList");
            if (index < 0 || index >= Count)
                throw new InvalidArgumentError($"Argument:({index}) is invalid.");

            Data[index] = item;
        }

        private void Resize(int newSize)
        {
            try
            {
                var newData = new T[newSize];
                for (var i = 0; i < Count; i++)
                    newData[i] = Data[i];
                Data = null;
                Data = newData;
            }
            catch (Exception e)
            {
                throw new ResizingError(e.Message);
            }
        }

        private int CheckResize()
        {
            if (Count != Data.Length / 4 || Data.Length / 2 == 0) return -1;
            Resize(Data.Length / 2);
            return 0;
        }

        public void AddFirst(T item)
        {
            if (IsReadOnly)
                throw new InvalidOperation("Can not Add item into a ReadOnly ArrayList");
            Insert(0, item);
        }

        public T Remove(int index)
        {
            if (IsReadOnly)
                throw new InvalidOperation("Can not Remove item into a ReadOnly ArrayList");
            if (index < 0 || index >= Count)
                throw new InvalidArgumentError("Remove failed. Index is invalid.");
            var ret = Data[index];
            for (long i = index + 1; i < Count; i++) Data[i - 1] = Data[i];

            Count--;
            // removed object should be GC data
            CheckResize();
            // After remove elements, if used size < capacity / 2 call resize.
            return ret;
        }

        public T RemoveFirst()
        {
            return Remove(0);
        }

        public T RemoveLast()
        {
            return Remove(Count - 1);
        }

        public T Get(int index)
        {
            if (index < 0 || index >= Count)
                throw new InvalidArgumentError("Get failed. Index is invalid.");

            return Data[index];
        }

        public T GetLast()
        {
            return Get(Count - 1);
        }

        public T GetFirst()
        {
            return Get(0);
        }

        public void Swap(int i, int j)
        {
            var tem = this[i];
            this[i] = this[j];
            this[j] = tem;
        }

        public override string ToString()
        {
#if DEBUG
            var sb = new StringBuilder();
            sb.Append($"MuxLib.MUtility.Collection.List.ArrayList<{typeof(T)}> Object");
            sb.Append('{');
            for (var i = 0; i < Count; i++)
            {
                sb.Append(Data[i]);
                if (i != Count - 1) sb.Append(", ");
            }

            sb.Append('}');
            return sb.ToString();
#else
            var sb = new StringBuilder();
            sb.Append($"MuxLib.MUtility.Collection.List.ArrayList<{typeof(T)}> Object");
            sb.Append('{');
            var time = Count > 20 ? 20 : Count;
            for (var i = 0; i < time; i++)
            {
                sb.Append(Data[i]);
                if (i != time - 1)
                {
                    sb.Append(", ");
                }
            }
            if (time != Count)
                sb.Append("...");
            sb.Append(Data[Count - 1]);
            sb.Append('}');
            return sb.ToString();
#endif
        }

        public void Sort(Compare compare)
        {
            Sort(compare, 0, Count - 1);
        }

        private void Sort(Compare compare, int low, int hight)
        {
            if (hight <= low) return;
            int lt = low, i = low + 1, gt = hight;
            var v = Data[low];
            while (i <= gt)
            {
                var cmp = compare(Data[i], v);
                switch (cmp)
                {
                    case < 0:
                        Swap(lt++, i++);
                        break;
                    case > 0:
                        Swap(i, gt--);
                        break;
                    default:
                        ++i;
                        break;
                }
            }

            Sort(compare, low, lt - 1);
            Sort(compare, gt + 1, hight);
        }
    }
}