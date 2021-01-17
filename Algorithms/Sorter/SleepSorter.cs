using System;
using System.Threading;

namespace MuxLib.MUtility.Algorithms.Sorter
{
    public class SleepSorter : Metas.Sorter<int>
    {
        public override void Sort(int[] arr)
        {
            for (var i = 0; i < arr.Length; i++)
            {
                var th = new Thread(start =>
                {
                    Thread.Sleep(arr[i] * 20);
                    Console.WriteLine(i);
                });
            }
        }
    }
}
