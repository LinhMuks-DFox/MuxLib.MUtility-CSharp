namespace MuxLib.MUtility.Algorithms.Sorter
{
    public sealed class ShellSorter<T> : Metas.Sorter<T>
        where T : System.IComparable
    {
        public override void Sort(T[] arr)
        {
            int N = arr.Length;
            int h = 1;
            while (h < N / 3) h = 3 * h + 1;
            while (h >= 1)
            {
                for (int i = h; i < N; ++i)
                {
                    for (int j = i; i >= h && Less(arr[j], arr[j - h]); j -= h)
                        Swap(arr, j, j - h);
                }
                h /= 3;
            }
        }
    }
}
