using System;
using MuxLib.MUtility.Algorithms.Sorter;
using MuxLib.MUtility.Algorithms.Scaler;
namespace Algorithms
{
    public static class Tester
    {
        const int size = 100000;
        static void Main(string[] args)
        {
            TestHeapSorter();
            TestSelectionSorter();
        }

        static void TestHeapSorter()
        {
            SorterScaler<int> scaler = new SorterScaler<int>();
            Random random = new Random(666);
            HeapSorter<int> heapSorter = new HeapSorter<int>();
            int[] arr = new int[size];
            for (int i = 0; i < size; ++i)
            {
                arr[i] = random.Next();
            }
            Console.WriteLine($"HeapSorter:\n\t{scaler.Runtime(arr, heapSorter)}");
            Console.WriteLine($"\tHeapSorter well done?:{heapSorter.IsSorted(arr, true)}");
        }

        static void TestSelectionSorter()
        {
            SorterScaler<int> scaler = new SorterScaler<int>();
            Random random = new Random(666);
            SelectionSorter<int> selector = new SelectionSorter<int>();
            int[] arr = new int[size];
            for (int i = 0; i < size; ++i)
            {
                arr[i] = random.Next();
            }
            Console.WriteLine($"SelectionSorter:\n\t{scaler.Runtime(arr, selector)}");
            Console.WriteLine($"\tSelectionSorter well done?:{selector.IsSorted(arr)}");
        }
    }
}
