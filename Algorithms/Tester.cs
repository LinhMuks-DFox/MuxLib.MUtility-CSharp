using System;
using MuxLib.MUtility.Algorithms.Sorter;
using MuxLib.MUtility.Algorithms.Scaler;
namespace MuxLib.MUtility.Algorithms
{
    public static class Tester
    {
        const int size = 100000;
        public static void TestHeapSorter()
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

        public static void TestSelectionSorter()
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

        public static void TestQuickSorter()
        {
            SorterScaler<int> scaler = new SorterScaler<int>();
            Random random = new Random(666);
            QuickSorter<int> selector = new QuickSorter<int>();
            int[] arr = new int[size];
            for (int i = 0; i < size; ++i)
            {
                arr[i] = random.Next();
            }
            Console.WriteLine($"QuickSorter:\n\t{scaler.Runtime(arr, selector)}");
            Console.WriteLine($"\tQuickSorter well done?:{selector.IsSorted(arr)}");
        }
    }
}
