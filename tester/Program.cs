using MuxLib.MUtility.Collections.List.ArrayList;
using System;
using MuxLib.MUtility.Algorithms;
using MuxLib.MUtility.Maths.basic;
namespace tester
{
    public class Program
    {
        public static void Main()
        {
            string path = @"D:\MuxLib\MuxLib.MUtility\tester\test.txt";
            TextReader textReader = new TextReader();
            textReader.Read(path);

            foreach (string s in textReader.Word)
            {
                Console.WriteLine(s);
            }
            Console.WriteLine(MMathConst.Pi);
        }
    }
}
