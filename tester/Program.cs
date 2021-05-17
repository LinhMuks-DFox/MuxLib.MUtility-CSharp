using System;
using MuxLib.MUtility.Maths.TinyLinearAlgebra;

namespace tester
{
    public static class Program
    {
        public static void Main()
        {
            var vectorV = new Vector(new double[2] {3, 4});
            var vectorU = new Vector(new double[2] {5, 6});
            Console.WriteLine(-vectorU);
        }
    }
}