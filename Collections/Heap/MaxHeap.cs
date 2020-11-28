using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuxLib.MUtility.Collections.Heap
{

    /// <summary>
    /// A Generic MaxHeap Class.
    /// </summary>
    /// <typeparam name="E">E should implement interface-ICompareable</typeparam>
    public sealed class MaxHeap<E>
        where E : IComparable
    {
        private List<E> _data;
        /// <summary>
        /// The capacity of Current Heap.
        /// </summary>
        public int Capaciy { get; } = 0;

        /// <summary>
        /// The count of current Heap
        /// </summary>
        public int Size { get => _data.Count; }

        /// <summary>
        /// Return True if current Heap contains no Element;
        /// </summary>
        public bool Empty { get => Size == 0; }

        public MaxHeap(E[] arr) /*Heapify*/
        {
            _data = new List<E>(arr);
            for (int i = FatherOf(arr.Length - 1); i >= 0; i--)
            {
                SiftDown(i);
            }
        }
        public MaxHeap(int prealloc)
        {
            _data = new List<E>(prealloc);
            Capaciy = prealloc;
        }
        /// <summary>
        /// Load a IEnumerable Object's elements in this Heap.
        /// </summary>
        /// <param name="from">Object which is Enumerable</param>
        public void Load(IEnumerable<E> from)
        {
            foreach (E i in from)
            {
                Add(i);
            }
        }
        public MaxHeap()
        {
            _data = new List<E>();
        }
        /// <summary>
        /// Sum the Father element's index for int-index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private int FatherOf(int index)
        {
            if (index == 0)
                throw new Errors.InvalidArgumentError("index -0 dose not have Father");
            return (index - 1) / 2;
        }

        private int LeftChildOf(int index)
        {
            return (index * 2) + 1;
        }

        private int RightChildOf(int index)
        {
            return (index * 2) + 2;
        }

        public void Add(E ele)
        {
            _data.Add(ele);
            SiftUp(Size - 1);
        }
        private void SiftUp(int k)
        {
            /*Find the father element of List[k], and check if his father is less the himself*/
            while (k > 0 && _data[FatherOf(k)].CompareTo(_data[k]) < 0)
            {
                /*Change the position of father and son; let father=son, son=father*/
                Swap(_data, k, FatherOf(k));
                k = FatherOf(k);
            }
        }

        private static void Swap(IList<E> list, int i, int j)
        {
            if (i < 0 || j < 0 || i >= list.Count || j >= list.Count)
                throw new Errors.InvalidArgumentError("Index is invalid");
            E t = list[i];
            list[i] = list[j];
            list[j] = t;
        }


        public E ExtractMax()
        {
            E ret = PeekMax();
            /*Swap the last element and the first element which will be removed*/
            Swap(_data, 0, _data.Count - 1);
            /*Remove the Lase element(which is the Max Element of current Heap)*/
            _data.RemoveAt(_data.Count - 1);
            /*Response to the nature of MaxHeap*/
            SiftDown(0);
            return ret;
        }

        public E PeekMax()
        {
            if (_data.Count == 0)
                throw new Errors.InvalidArgumentError("Heap is Empty");
            return _data[0];
        }
        private void SiftDown(int k)
        {
            while (LeftChildOf(k) < _data.Count)
            {
                int j = LeftChildOf(k);
                /*Find the max one of heap[k]'s left child and right child.*/
                if (j + 1 < _data.Count && _data[j + 1].CompareTo(_data[j]) > 0)
                    j = RightChildOf(k); /*Let j points to the Right child of Heap[k]*/
                if (_data[k].CompareTo(_data[j]) >= 0)
                    break;

                Swap(_data, k, j);
                k = j;
            }
        }

        public E Replace(E e)
        {
            E ret = PeekMax();
            _data[0] = e;
            SiftDown(0);
            return ret;
        }
        public override string ToString()
        {
            return $"MaxHeap{typeof(E)} Instance";
        }

        public static void Tester()
        {
            int n = 1000000;
            MaxHeap<int> maxHeap = new MaxHeap<int>(n);
            Random random = new Random();
            for (int i = 0; i < n; i++)
            {
                maxHeap.Add(random.Next(int.MaxValue));
            }

            int[] arr = new int[n];
            for (int i = 0; i < n; i++)
            {
                arr[i] = maxHeap.ExtractMax();
            }


            for (int i = 1; i < n; i++)
            {
                if (arr[i - 1] < arr[i])
                {
                    Console.WriteLine("Error!");
                    break;
                }
            }
            Console.WriteLine("Well Done!");
        }

        public E[] ToArray()
        {
            return _data.ToArray();
        }
    }

}
