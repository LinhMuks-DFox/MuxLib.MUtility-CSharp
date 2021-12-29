using System;
using System.Collections.Generic;
using MuxLib.MUtility.Collections.List.ArrayList;
using MuxLib.MUtility.Maths.TinyLinearAlgebra;

namespace tester
{
    public static class Program
    {
        public static void Main()
        {
            var list = new ArrayList<int>(10);
            list.Sort((int i, int j) => i - j);
        }
    }
}