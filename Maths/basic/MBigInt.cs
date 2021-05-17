using System.Collections.Generic;
using MuxLib.MUtility.Com.Attr;

namespace MuxLib.MUtility.Maths.basic
{
    [Unimplemented]
    public class MBigInt
    {
        public const int MShift = 1073741824; // 2 ** 30
        private List<int> _digits;
        private Signs _sign;

        private MBigInt(Signs sig = Signs.NaN)
        {
            _digits = new List<int>();
            _sign = sig;
        }

        public MBigInt(ulong ul) : this(Signs.Positives)
        {
        }

        public MBigInt(double d) : this((long) d)
        {
        }

        public MBigInt(long l) : this(l >= 0 ? Signs.Positives : Signs.Negative)
        {
        }

        public static implicit operator MBigInt(int ival)
        {
            return new(ival);
        }

        public static implicit operator MBigInt(uint uival)
        {
            return new( /*(long)*/uival);
        }

        public static implicit operator MBigInt(double dval)
        {
            return new(dval);
        }

        public static implicit operator MBigInt(float fval)
        {
            return new(fval);
        }

        public static implicit operator MBigInt(long lval)
        {
            return new(lval);
        }
    }
}