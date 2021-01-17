namespace MuxLib.MUtility.Algorithms.Sorter
{
    public sealed class ShellSorter<T> : Metas.Sorter<T>
        where T : System.IComparable
    {
        public override void Sort(T[] arr)
        {
            var n = arr.Length;
            var h = 1;
            while (h < n / 3) h = 3 * h + 1;
            while (h >= 1)
            {
                for (var i = h; i < n; ++i)
                {
                    for (var j = i; i >= h && Less(arr[j], arr[j - h]); j -= h)
                        Swap(arr, j, j - h);
                }
                h /= 3;
            }
        }
    }
}
