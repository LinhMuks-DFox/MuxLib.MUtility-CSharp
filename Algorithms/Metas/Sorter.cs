namespace MuxLib.MUtility.Algorithms.Metas
{
    public abstract class Sorter<T>
        where T : System.IComparable
    {
        public abstract void Sort(T[] arr);

        public bool Less(T v, T w)
            => v.CompareTo(w) < 0;

        public static void Swap(T[] arr, int i, int j)
        {
            T t = arr[i];
            arr[i] = arr[j];
            arr[j] = t;
        }

        public static void Show(T[] arr)
        {
            System.Console.Write("{");
            for (int i = 0; i < arr.Length; i++)
            {
                System.Console.Write(arr[i]);
                if (i != arr.Length - 1)
                    System.Console.Write(", ");
            }
            System.Console.Write("}\n");
        }

        public bool IsSorted(T[] arr, bool big_to_small = false)
        {
            if (big_to_small)
            {
                for (int i = 1; i < arr.Length; ++i)
                {
                    if (Less(arr[i - 1], arr[i])) return false;
                }
                return true;
            }
            else /*small->big*/
            {
                for (int i = 1; i < arr.Length; ++i)
                {
                    if (Less(arr[i], arr[i - 1])) return false;
                }
                return true;
            }
        }
    }
}
