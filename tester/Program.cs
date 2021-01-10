using MuxLib.MUtility.Collections.Set;
using System;
namespace tester
{
    public class Program
    {
        public static void Main()
        {
            string path = @"D:\MuxLib\MuxLib.MUtility\tester\pride-and-prejudice.txt";
            TextReader textReader = new TextReader();
            textReader.Read(path);

            AVLSet<string> tree = new AVLSet<string>();

            foreach (string str in textReader.Word)
            {
                tree.Add(str);
            }

            Console.WriteLine(tree.Size);
        }
    }
}
