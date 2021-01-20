using System;
using System.Diagnostics;
using MuxLib.MUtility.Algorithms.Metas;

namespace MuxLib.MUtility.Algorithms.Scaler
{
    public sealed class SorterScaler<T>
        where T : IComparable
    {
        public double RuntimeMs { get; private set; }
        public double RuntimeS { get; private set; }


        public string CalRuntime(T[] testArr, Sorter<T> sorter)
        {
            var sw = new Stopwatch();
            sw.Start();
            sorter.Sort(testArr);
            sw.Stop();
            RuntimeMs = sw.Elapsed.TotalMilliseconds;
            RuntimeS = sw.Elapsed.TotalSeconds;
            return $"Totally ran {RuntimeMs}ms, or {RuntimeS}s";
        }
    }
}