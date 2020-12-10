namespace MuxLib.MUtility.Algorithms.Scaler
{
    public sealed class SorterScaler<T>
        where T : System.IComparable
    {
        public string Runtime(T[] testarr, Metas.Sorter<T> sorter)
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            sorter.Sort(testarr);
            sw.Stop();
            return $"Totally ran {sw.Elapsed.TotalMilliseconds}ms, or {sw.Elapsed.TotalSeconds}s";
        }
    }
}
