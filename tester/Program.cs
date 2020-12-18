using MuxLib.MUtility.Collections.List.ArrayList;
using System;
namespace tester
{
    public class Program
    {
        public static void Main()
        {
            Random random = new Random(666);
            ArrayList<int> array = new ArrayList<int>(1000);
            for (int i = 0; i < 1000; i++)
            {
                array.Add(random.Next() % 100);
            }

            array.Sort(new ArrayList<int>.Compare((int item1, int item2) => item1 - item2));
            Console.WriteLine(array);
        }
    }
}
