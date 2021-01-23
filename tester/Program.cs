using System;
using MuxLib.MUtility.GraphTheoryAlgorithm;

namespace tester
{
    public static class Program
    {
        public static void Main()
        {
            var graph = new Graph(@"E:\MuxLib\MuxLib.MUtility.Sharp\tester\g.txt");
            var dfs = new GraphDfs(graph);
            foreach (var ele in dfs.Order)
                Console.WriteLine(ele);
        }
    }
}