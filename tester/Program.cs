using System;
using MuxLib.MUtility.Collections.List.ArrayList;
namespace tester
{
    class Program
    {
        static void Main(string[] args)
        {
            ArrayList<int> datas = new ArrayList<int>(10);
            for (int i = 0; i < 20; i++)
                datas.Add(i);
            Console.WriteLine(datas);
            datas[4] = 20;
            Console.WriteLine(datas);
            Console.WriteLine(datas.Count);
            // datas.Clear();
            datas.RemoveAt(datas.IndexOf(3));
            Console.WriteLine(datas);
            Console.WriteLine(datas.Count);
        }
    }
}
