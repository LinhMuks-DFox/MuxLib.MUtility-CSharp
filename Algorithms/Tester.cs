using System;
using MuxLib.MUtility.Algorithms.Scaler;
using MuxLib.MUtility.Algorithms.Sorter;

namespace MuxLib.MUtility.Algorithms
{
    public static class Tester
    {
        private const int Size = 100000;

        public static void TestHeapSorter()
        {
            var scaler = new SorterScaler<int>();
            var random = new Random(666);
            var heapSorter = new HeapSorter<int>();
            var arr = new int[Size];
            for (var i = 0; i < Size; ++i) arr[i] = random.Next();
            Console.WriteLine($"HeapSorter:\n\t{scaler.CalRuntime(arr, heapSorter)}");
            Console.WriteLine($"\tHeapSorter well done?:{heapSorter.IsSorted(arr, true)}");
        }

        public static void TestSelectionSorter()
        {
            var scaler = new SorterScaler<int>();
            var random = new Random(666);
            var selector = new SelectionSorter<int>();
            var arr = new int[Size];
            for (var i = 0; i < Size; ++i) arr[i] = random.Next();
            Console.WriteLine($"SelectionSorter:\n\t{scaler.CalRuntime(arr, selector)}");
            Console.WriteLine($"\tSelectionSorter well done?:{selector.IsSorted(arr)}");
        }

        public static void TestQuickSorter()
        {
            var scaler = new SorterScaler<int>();
            var random = new Random(666);
            var quick = new QuickSorter<int>();
            var arr = new int[Size];
            for (var i = 0; i < Size; ++i) arr[i] = random.Next();
            Console.WriteLine($"QuickSorter:\n\t{scaler.CalRuntime(arr, quick)}");
            Console.WriteLine($"\tQuickSorter well done?:{quick.IsSorted(arr)}");
        }

        public static void TestQuantumBogoSorter()
        {
            var scaler = new SorterScaler<int>();
            var random = new Random(666);
            var quantumBogo = new QuantumBogoSorter<int>();
            var arr = new int[10];
            for (var i = 0; i < arr.Length; ++i) arr[i] = random.Next() % 10;
            Console.WriteLine($"QuantumBogoSorter:\n\t{scaler.CalRuntime(arr, quantumBogo)}");
            Console.WriteLine($"\tQuantumBogoSorter well done?:{quantumBogo.IsSorted(arr)}");
        }
    }
}