using MuxLib.MUtility.Com.Errors;

namespace MuxLib.MUtility.Maths.TinyLinearAlgebra
{
    public class Matrix
    {
        private double[,] Value { get; }

        public Matrix(double[,] value)
        {
            Value = value;
        }

        public static Matrix Zero(int[] dim)
        {
            if (dim.Length > 2)
            {
                throw new InvalidArgumentError("TinyLinearAlgebra.Matrix only produce 2-dim Matrix");
            }

            return new Matrix(new double[dim[0], dim[1]]);
        }

        public static Matrix Identity(int n)
        {
            var t = new double[n, n];
            for (var i = 0; i < n; ++i)
                t[i, i] = 1;
            return new Matrix(t);
        }
    }
}