using MuxLib.MUtility.Collections.Set;
using MuxLib.MUtility.Collections.Tree.BST;
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

            BST<string, object> tree = new BST<string, object>();

            foreach (string str in textReader.Word)
            {
                tree[str] = null;
            }
            Console.WriteLine(tree.Size);
        }
    }
}
