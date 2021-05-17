using System;
using System.Threading;
using MuxLib.MUtility.Algorithms.Metas;

namespace MuxLib.MUtility.Algorithms.Sorter
{
    public class SleepSorter : Sorter<int>
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
                th.Start();
            }
        }
    }
}