using System.Globalization;

namespace Utils
{
    public static class SharedUtils
    {
        public const bool IsMobile = true;

        public static bool IsNull(object obj)
        {
            return obj == null;
        }

        public static string DoubleToString(double value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }
    }
}
