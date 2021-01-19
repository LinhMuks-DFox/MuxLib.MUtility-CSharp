using System;

namespace MuxLib.MUtility.Algorithms.Metas
{
    public abstract class Sorter<T>
        where T : IComparable
    {
        public abstract void Sort(T[] arr);

        protected bool Less(T v, T w)
        {
            return v.CompareTo(w) < 0;
        }

        protected static void Swap(T[] arr, int i, int j)
        {
            var t = arr[i];
            arr[i] = arr[j];
            arr[j] = t;
        }

        public static void Show(T[] arr)
        {
            Console.Write("{");
            for (var i = 0; i < arr.Length; i++)
            {
                Console.Write(arr[i]);
                if (i != arr.Length - 1)
                    Console.Write(", ");
            }

            Console.Write("}\n");
        }

        public bool IsSorted(T[] arr, bool bigToSmall = false)
        {
            if (bigToSmall)
            {
                for (var i = 1; i < arr.Length; ++i)
                    if (Less(arr[i - 1], arr[i]))
                        return false;
                return true;
            }

            for (var i = 1; i < arr.Length; ++i)
                if (Less(arr[i], arr[i - 1]))
                    return false;
            return true;
        }
    }
}