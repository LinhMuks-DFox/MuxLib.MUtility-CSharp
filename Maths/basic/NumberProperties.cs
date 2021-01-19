namespace MuxLib.MUtility.Maths.basic
{
    internal enum Signs
    {
        Positives,
        Negative,
        NaN
    }


    public abstract class NumberHelper
    {
        public static bool IsValidDigitInBase(char ch, int n_cimal)
        {
            return n_cimal switch
            {
                2 => '0' == ch || '1' == ch,
                8 => '0' <= ch && ch <= '7',
                10 => '0' <= ch && ch <= '9',
                16 => '0' <= ch && ch <= '9' || 'a' <= ch && ch <= 'f' || 'A' <= ch && ch <= 'F',
                _ => false
            };
        }

        public static int CharToInt(char ch)
        {
            switch (ch)
            {
                case <= '0' and <= '9':
                    return ch - '0';
                case <= 'a' and <= 'f':
                    return ch - 'a' + 10;
                default:
                {
                    if ('A' <= ch && ch <= 'F')
                        return ch - 'A' + 10;
                    return -1;
                }
            }
        }
    }
}