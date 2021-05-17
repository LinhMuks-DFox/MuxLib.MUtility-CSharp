using System.Runtime.CompilerServices;

namespace MuxLib.MUtility.Maths.basic
{
    public static class MMathConst
    {
        /* e */
        public static decimal E => 2.718281828459045235360287471352662498M;

        /* log_2 e */
        public static decimal Log2E => 1.442695040888963407359924681001892137M;

        /* log_10 e*/
        public static decimal Log10E => 0.434294481903251827651128918916605082M;

        /* log_e 2 */
        public static decimal LogE2 => 0.693147180559945309417232121458176568M;

        /* log_e 10 */
        public static decimal LogE10 => 2.302585092994045684017991454684364208M;

        /* pi */
        public static decimal Pi => 3.141592653589793238462643383279502884M;

        /* pi / 2 */
        public static decimal Pi2 => 1.570796326794896619231321691639751442M;

        /* pi / 4 */
        public static decimal Pi4 => 0.785398163397448309615660845819875721M;

        /* 1 / pi */
        public static decimal M1Pi => 0.318309886183790671537767526745028724M;

        /* 2 / pi */
        public static decimal M2Pi => 0.636619772367581343075535053490057448M;

        /* Euler constant */
        public static decimal Euler => 0.577215664901532860606512090082402431M;

        /* sqrt(2) */
        public static decimal Sqrt2 => 1.414213562373095048801688724209698079M;

        /* 1 / sqrt(2) */
        public static decimal M1Sqrt2 => 0.707106781186547524400844362104849039M;

        public static decimal Epsilon => 1e-8M;

        public static bool IsZero(double f)
        {
            return System.Math.Abs(f) < (double) Epsilon;
        }

        public static bool IsEqual(double f, double d)
        {
            return System.Math.Abs(f - d) < (double) Epsilon;
        }
    }
}