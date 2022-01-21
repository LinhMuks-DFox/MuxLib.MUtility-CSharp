using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MuxLib.MUtility.Com.Errors;
using MuxLib.MUtility.Maths.basic;
namespace MuxLib.MUtility.Maths.TinyLinearAlgebra
{
    public class Vector : IEnumerable<double>
    {
        private double[] Value { get; }

        public int Length => Value.Length;

        public Vector(double[] array)
        {
            Value = array;
        }

        private static Vector Zero(int dim)
        {
            return new(new double[dim]);
        }

        public double Norm()
        {
            var t = from d in Value
                select Math.Pow(d, 2);
            var s = t.ToArray().Sum();
            return Math.Sqrt(s);
        }

        public static Vector operator +(Vector v, Vector u)
        {
            if (v.Length != u.Length)
                throw new InvalidOperation(
                    $"Error in adding. Length of vectors must be same. but {v.Length}, {u.Length}");
            var t = new double[v.Length];
            for (var k = 0; k < v.Length; ++k)
                t[k] = v.Value[k] + u.Value[k];
            return new Vector(t);
        }

        public static Vector operator -(Vector v, Vector u)
        {
            if (v.Length != u.Length)
                throw new InvalidOperation(
                    $"Error in Sub-operation. Length of vectors must be same. but {v.Length}, {u.Length}");
            var t = new double[v.Length];
            for (var k = 0; k < v.Length; ++k)
                t[k] = v.Value[k] - u.Value[k];
            return new Vector(t);
        }

        public static Vector operator /(Vector v, double k)
        {
            return v * (1 / k);
        }

        public static Vector operator *(Vector v, double k)
        {
            return new((from d in v.Value select d * k).ToArray());
        }

        public static Vector operator *(double k, Vector v)
        {
            return v * k;
        }

        public static Vector operator -(Vector v)
        {
            return -1 * v;
        }

        public static Vector operator +(Vector v)
        {
            return 1 * v;
        }

        public static double operator *(Vector v, Vector u)
        {
            return v.Dot(u);
        }

        public IEnumerator<double> GetEnumerator()
        {
            return (IEnumerator<double>) Value.GetEnumerator();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
#if DEBUG
            sb.Append("MuxLib.MUtility.Maths.TinyLinearAlgebra instance, contains:{");
#else
            sb.Append('{');
#endif
            for (var i = 0; i < Value.Length; ++i)
            {
                sb.Append(Value[i]);
                if (i != Value.Length - 1) sb.Append(", ");
            }

            sb.Append('}');

            return sb.ToString();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Vector Normalize()
        {
            if (MMathConst.IsZero(Norm()))
                throw new DivideByZeroException("Normalize error! norm is zero.");
            return new Vector(Value) / Norm();
        }

        // sum(a * b for a, b in zip(self, another))
        public double Dot(Vector u)
        {
            var t = new double [u.Length];

            for (var i = 0; i < u.Length; ++i) t[i] = u.Value[i] * Value[i];

            return t.Sum();
        }


        public double[] UnderLyingArray => (from d in Value select d).ToArray();
    }
}