using System;
using System.Diagnostics;
using MuxLib.MUtility.Algorithms.Metas;

namespace MuxLib.MUtility.Algorithms.Scaler
{
    public sealed class SorterScaler<T>
        where T : IComparable
    {
        public double RumtimeMs { get; private set; }
        public double RumtimeS { get; private set; }


        public string CalRuntime(T[] testarr, Sorter<T> sorter)
        {
            var sw = new Stopwatch();
            sw.Start();
            sorter.Sort(testarr);
            sw.Stop();
            RumtimeMs = sw.Elapsed.TotalMilliseconds;
            RumtimeS = sw.Elapsed.TotalSeconds;
            return $"Totally ran {RumtimeMs}ms, or {RumtimeS}s";
        }
    }
}