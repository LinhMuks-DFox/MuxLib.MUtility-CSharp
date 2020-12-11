namespace MuxLib.MUtility.Algorithms.Sorter
{
    internal sealed class SelectionSorter<T> : Metas.Sorter<T>
        where T : System.IComparable
    {

        public override void Sort(T[] arr)
        {
            int N = arr.Length;
            for (int i = 0; i < N; ++i)
            {
                int min = i;
                for (int j = i + 1; j < N; j++)
                    if (Less(arr[j], arr[min])) min = j;
                Swap(arr, i, min);
            }
        }
    }
}
