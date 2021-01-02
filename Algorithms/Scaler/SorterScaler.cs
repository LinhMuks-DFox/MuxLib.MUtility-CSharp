namespace MuxLib.MUtility.Algorithms.Scaler
{
    public sealed class SorterScaler<T>
        where T : System.IComparable
    {
        public double RumtimeMs { get; private set; } = default;
        public double RumtimeS { get; private set; } = default;


        public string CalRuntime(T[] testarr, Metas.Sorter<T> sorter)
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            sorter.Sort(testarr);
            sw.Stop();
            RumtimeMs = sw.Elapsed.TotalMilliseconds; RumtimeS = sw.Elapsed.TotalSeconds;
            return $"Totally ran {RumtimeMs}ms, or {RumtimeS}s";
        }
    }
}
