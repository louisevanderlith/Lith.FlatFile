using System;

namespace Lith.FlatFile.Core
{
    public static class Stringer
    {
        public static string Transform(this Enum value, int length)
        {
            var iValue = value.GetHashCode();

            return iValue.ToString().Pad(true, length);
        }

        public static string Transform(this DateTime value, string format)
        {
            return value.ToString(format).Pad(false, format.Length);
        }

        public static string Transform(this decimal value, int fieldLength, int decimalPlaces)
        {
            var multiplier = FlatteningHelper.GetDecimalMultiplier(decimalPlaces);
            var result = value * multiplier;
            var rounded = Math.Round(result, 0, MidpointRounding.AwayFromZero).ToString();

            return rounded.Pad(true, fieldLength);
        }

        public static string Transform(this bool value, string trueValue, string falseValue)
        {
            var padLength = trueValue.Length >= falseValue.Length ? trueValue.Length : falseValue.Length;
            var result = value ? trueValue : falseValue;

            return result.Pad(false, padLength);
        }

        public static string Pad(this string item, bool isNumeric, int width)
        {
            var result = string.Empty;

            if (isNumeric)
            {
                result = item.PadLeft(width, '0');
            }
            else
            {
                result = item.PadRight(width, ' ');
            }

            return result;
        }
    }
}
