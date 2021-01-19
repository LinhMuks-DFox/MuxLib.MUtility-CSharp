using System;
using MuxLib.MUtility.GraphTheoryAlgorithm;

namespace tester
{
    public static class Program
    {
        public static void Main()
        {
            var adjMatrix = new AdjMatrix(@"E:\MuxLib\MuxLib.MUtility.Sharp\tester\g.txt");
            Console.WriteLine(adjMatrix);
        }
    }
}