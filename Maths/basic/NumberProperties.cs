using System;
using System.Collections.Generic;
using System.Text;

namespace MuxLib.MUtility.Maths.basic
{
    enum Signs
    {
        Postive,
        Negativ,
        NaN
    }


    public abstract class NumberHelper
    {
        public static bool IsValidDigitInBase(char ch, int n_cimal)
        {
            switch (n_cimal)
            {
                case 2:
                    return ('0' == ch || '1' == ch);
                case 8:
                    return ('0' <= ch && ch <= '7');
                case 10:
                    return ('0' <= ch && ch <= '9');
                case 16:
                    return ('0' <= ch && ch <= '9') || ('a' <= ch && ch <= 'f') || ('A' <= ch && ch <= 'F');
                default:
                    return false;
            }
        }

        public static int CharToInt(char ch)
        {
            if ('0' <= ch && ch <= '9')
                return ch - '0';
            else if ('a' <= ch && ch <= 'f')
                return ch - 'a' + 10;
            else if ('A' <= ch && ch <= 'F')
                return ch - 'A' + 10;
            else
                return -1;
        }
    }
}
