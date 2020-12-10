using System;
using MuxLib.MUtility.Collections.Tree.AVLTree;
namespace tester
{
    class Program
    {
        static void Main(string[] args)
        {
            AVLTree<int, string> tree = new AVLTree<int, string>();
            tree.Append(1, "1st");
            tree.Append(2, "2nd");
            Console.WriteLine(tree[1]);
        }
    }
}
