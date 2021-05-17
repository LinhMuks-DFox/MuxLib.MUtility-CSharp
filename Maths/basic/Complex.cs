using System;
using System.Runtime.InteropServices;

namespace MuxLib.MUtility.Maths.basic
{
    public sealed class Complex
    {
        public Complex(decimal real, decimal image)
        {
            Real = real;
            Image = image;
        }

        public Complex(Complex complex) : this(complex.Real, complex.Image)
        {
        }

        public override string ToString()
        {
            return $"{Real}+{Image}j";
        }

        public decimal Real { get; }

        public decimal Image { get; }

        public static Complex operator +(Complex i, Complex j)
        {
            return new Complex(i.Real + j.Real, i.Image + j.Image);
        }

        public static Complex operator -(Complex i, Complex j)
        {
            return new Complex(i.Real - j.Real, i.Image - j.Image);
        }

        // (a+bi)(c+di)=(ac-bd)+(bc+ad)i.
        public static Complex operator *(Complex i, Complex j)
        {
            return new Complex(i.Real * j.Real - i.Image * j.Image,
                i.Image * j.Real + i.Real + j.Image);
        }


        public static implicit operator Complex(int val)
        {
            return new Complex(val, val);
        }

        public static implicit operator Complex(long val)
        {
            return new Complex(val, val);
        }

        public static implicit operator Complex(short val)
        {
            return new Complex(val, val);
        }

        public static implicit operator Complex(uint val)
        {
            return new Complex(val, val);
        }

        public static implicit operator Complex(ulong val)
        {
            return new Complex(val, val);
        }

        public static implicit operator Complex(ushort val)
        {
            return new Complex(val, val);
        }

        public static implicit operator Complex(decimal val)
        {
            return new Complex(val, val);
        }

        public static implicit operator Complex(float val)
        {
            return new Complex((decimal) val, (decimal) val);
        }

        public static implicit operator Complex(double val)
        {
            return new Complex((decimal) val, (decimal) val);
        }
    }
}