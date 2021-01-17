using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MuxLib.MUtility.Collections.List.ArrayList
{
    public sealed class ArrayList<T> : IList<T>, ICollection<T>
    {
        private T[] _data;
        private int _size;
        public bool IsEmpty => Count == 0;


        public int Count => _size;

        public bool IsReadOnly { set; get; } = false;

        public T this[int index]
        {
            get => Get(index);
            set
            {
                if (index == _size) Insert(_size, value);
                else Set(index, value);
            }
        }

        public ArrayList()
        {
            _size = 0;
            _data = new T[500];
        }

        public ArrayList(int capacity)
        {
            _size = 0;
            _data = new T[capacity];
        }

        public ArrayList(T[] data)
        {
            _size = 0;
            _data = new T[data.Length];
            for (var i = 0; i < data.Length; i++)
            {
                _data[i] = data[i];
            }
        }

        public void Set(int index, T item)
        {
            if (IsReadOnly)
                throw new Errors.InvalidOperation("Can not set item into a ReadOnly ArrayList");
            if (index < 0 || index >= _size)
                throw new Errors.InvalidArgumentError($"Argument:({index}) is invalid.");

            _data[index] = item;
        }

        private void Resize(int newSize)
        {
            try
            {
                var newData = new T[newSize];
                for (var i = 0; i < _size; i++)
                    newData[i] = _data[i];
                _data = null; _data = newData;
            }
            catch (Exception e)
            {
                throw new Errors.ResizingError(e.Message);
            }
        }

        private int CheckResize()
        {
            if (_size != _data.Length / 4 || _data.Length / 2 == 0) return -1;
            Resize(_data.Length / 2);
            return 0;
        }

        public int IndexOf(T item)
        {
            for (var i = 0; i < _size; i++)
            {
                if (_data[i].Equals(item))
                    return i;
            }
            return -1;
        }

        public void Insert(int index, T item)
        {
            if (IsReadOnly)
                throw new Errors.InvalidOperation("Can not insert item into a ReadOnly ArrayList");
            if (_size.Equals(_data.Length))
                Resize(2 * _data.Length);

            if (index < 0 || index > _size)
                throw new Errors.InvalidArgumentError
                            ("Insert failed. Invalid Index was passed." +
                            "Index should >= 0 and <= size");
            for (long i = _size - 1; i >= index; i--)
                _data[i + 1] = _data[i];
            _data[index] = item; _size++;
        }

        public void RemoveAt(int index)
        {
            if (IsReadOnly)
                throw new Errors.InvalidOperation("Can not Remove item into a ReadOnly ArrayList");
            if (index < 0 || index >= _size)
                throw new Errors.InvalidArgumentError("Remove failed. Index is invalid.");
            for (var i = index + 1; i < _size; i++)
                _data[i - 1] = _data[i];
            _size--;
            CheckResize();
        }

        public void Add(T item)
        {
            if (IsReadOnly)
                throw new Errors.InvalidOperation("Can not Add item into a ReadOnly ArrayList");
            Insert(Count, item);
        }

        public void AddFirst(T item)
        {
            if (IsReadOnly)
                throw new Errors.InvalidOperation("Can not Add item into a ReadOnly ArrayList");
            Insert(0, item);
        }

        public void Clear()
        {
            if (IsReadOnly)
                throw new Errors.InvalidOperation("Can not Clear item into a ReadOnly ArrayList");
            _size = 0;
            _data = new T[_data.Length];
        }

        public bool Contains(T item)
        {
            return IndexOf(item) != -1;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (arrayIndex < 0 || arrayIndex >= _size)
                throw new Errors.InvalidArgumentError($"Argument:{arrayIndex} is invalid");
            for (var i = 0; i < arrayIndex; i++)
                array[i] = _data[i];
        }

        public bool Remove(T item)
        {
            if (IsReadOnly)
                throw new Errors.InvalidOperation("Can not Remove item into a ReadOnly ArrayList");
            var index = IndexOf(item);
            if (index == -1) return false;
            Remove(index);
            return true;

        }

        public T Remove(int index)
        {
            if (IsReadOnly)
                throw new Errors.InvalidOperation("Can not Remove item into a ReadOnly ArrayList");
            if (index < 0 || index >= _size)
                throw new Errors.InvalidArgumentError("Remove failed. Index is invalid.");
            var ret = _data[index];
            for (long i = index + 1; i < _size; i++)
            {
                _data[i - 1] = _data[i];
            }
            _size--;
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
            return Remove(_size - 1);
        }
        public T Get(int index)
        {
            if (index < 0 || index >= _size)
                throw new Errors.InvalidArgumentError("Get failed. Index is invalid.");

            return _data[index];
        }

        public T GetLast()
        {
            return Get(_size - 1);
        }

        public T GetFirst()
        {
            return Get(0);
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (var i = 0; i < _size; i++)
            {
                yield return Get(i);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for (var i = 0; i < _size; i++)
            {
                yield return Get(i);
            }
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
            StringBuilder sb = new StringBuilder();
            sb.Append($"MuxLib.MUtility.Collection.List.ArrayList<{typeof(T)}> Object");
            sb.Append("{");
            for (int i = 0; i < _size; i++)
            {
                sb.Append(_data[i].ToString());
                if (i != _size - 1)
                {
                    sb.Append(", ");
                }
            }
            sb.Append("}");
            return sb.ToString();
#else
            StringBuilder sb = new StringBuilder();
            sb.Append($"MuxLib.MUtility.Collection.List.ArrayList<{typeof(T)}> Object");
            sb.Append('{');
            int time = _size > 20 ? 20 : _size;
            for (int i = 0; i < time; i++)
            {
                sb.Append(_data[i].ToString());
                if (i != time - 1)
                {
                    sb.Append(", ");
                }
            }
            if (time != _size)
                sb.Append("...");
            sb.Append(_data[_size - 1]);
            sb.Append('}');
            return sb.ToString();
#endif
        }

        public delegate int Compare(T item1, T item2);

        public void Sort(Compare compare)
        {
            Sort(compare, 0, _size - 1);
        }

        private void Sort(Compare compare, int low, int hight)
        {
            if (hight <= low) return;
            int lt = low, i = low + 1, gt = hight;
            var v = _data[low];
            while (i <= gt)
            {
                var cmp = compare(_data[i], v);
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
