using System;
using MuxLib.MUtility.Algorithms.Metas;

namespace MuxLib.MUtility.Algorithms.Sorter
{
    public class QuantumBogoSorter<T> : Sorter<T>
        where T : IComparable
    {
        public override void Sort(T[] arr)
        {
            var random = new Random();
            while (!IsSorted(arr)) Shuffle(arr, random);
        }

        private void Shuffle(T[] arr, Random random)
        {
            for (var i = 0; i < arr.Length; i++)
            {
                // ensure the random value will not "out-of-range"
                var j = Math.Abs(random.Next() % arr.Length - 1);

                Swap(arr, i, j);
            }
        }
    }
}