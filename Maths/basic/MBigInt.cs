using System;
using System.Collections.Generic;
using MuxLib.MUtility.Com.Attr;
namespace MuxLib.MUtility.Maths.basic
{
    [Unimplemented]
    public class MBigInt
    {
        public const int M_SHIFT = 1073741824; // 2 ** 30
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
        public MBigInt(double d) : this((long)d)
        {
        }

        public MBigInt(long l) : this(l >= 0 ? Signs.Positives : Signs.Negative)
        {

        }

        public static implicit operator MBigInt(int ival)
        {
            return new MBigInt(ival);
        }

        public static implicit operator MBigInt(uint uival)
        {
            return new MBigInt(/*(long)*/uival);
        }

        public static implicit operator MBigInt(double dval)
        {
            return new MBigInt(dval);
        }

        public static implicit operator MBigInt(float fval)
        {

            return new MBigInt(fval);
        }

        public static implicit operator MBigInt(long lval)
        {
            return new MBigInt(lval);
        }
    }
}
