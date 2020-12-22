using System;
namespace MuxLib.MUtility.Algorithms.Sorter
{
    public class MonkeySorter<T> : Metas.Sorter<T>
        where T : IComparable
    {
        public override void Sort(T[] arr)
        {
            Random random = new Random();
            while (!IsSorted(arr))
            {
                Shuffle(arr, random);
            }
        }

        private void Shuffle(T[] arr, Random random)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                int j = random.Next();

                Swap(arr, i, j);
            }
        }
    }
}
