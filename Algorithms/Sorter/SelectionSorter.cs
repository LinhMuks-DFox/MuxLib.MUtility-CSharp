namespace MuxLib.MUtility.Algorithms.Sorter
{
    internal sealed class SelectionSorter<T> : Metas.Sorter<T>
        where T : System.IComparable
    {

        public override void Sort(T[] arr)
        {
            var n = arr.Length;
            for (var i = 0; i < n; ++i)
            {
                var min = i;
                for (var j = i + 1; j < n; j++)
                    if (Less(arr[j], arr[min])) min = j;
                Swap(arr, i, min);
            }
        }
    }
}
