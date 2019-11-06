using Lith.FlatFile.Core;
using System;

namespace Lith.FlatFile
{
    public static class DeStringer
    {
        public static T ToEnum<T>(this string value)
        {
            var isValid = int.TryParse(value, out int trimmed);

            if (!isValid)
            {
                trimmed = 0;
            }
            
            var enumObj = Enum.Parse(typeof(T), trimmed.ToString());

            return (T)enumObj;
        }

        public static DateTime ToDateTime(this string value, string format)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return DateTime.MinValue;
            }

            var yearPart = FlatteningHelper.GetPointValue(format, 'y', value);
            var monthPart = FlatteningHelper.GetPointValue(format, 'M', value);
            var dayPart = FlatteningHelper.GetPointValue(format, 'd', value);
            var time = GetTimePart(value, format);

            var date = string.Format("{0}/{1}/{2}{3}", yearPart, monthPart, dayPart, time);

            DateTime.TryParse(date, out DateTime result);

            return result;
        }

        private static string GetTimePart(string rawDate, string format)
        {
            var result = string.Empty;

            if (format.Contains("HHmmss"))
            {
                var hourPart = FlatteningHelper.GetPointValue(format, 'H', rawDate);
                var minutePart = FlatteningHelper.GetPointValue(format, 'm', rawDate);
                var secondsPart = FlatteningHelper.GetPointValue(format, 's', rawDate);

                result = string.Format(" {0}:{1}:{2}", hourPart, minutePart, secondsPart);
            }

            return result;
        }

        public static decimal ToDecimal(this string value, int decimalPlaces)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return 0M;
            }

            var multiplier = FlatteningHelper.GetDecimalMultiplier(decimalPlaces);
            var iValue = decimal.Parse(value);
            var divided = iValue / multiplier;

            var result = Math.Round(divided, decimalPlaces, MidpointRounding.AwayFromZero);

            return result;
        }

        public static bool ToBool(this string value, string trueValue)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return false;
            }

            var cleanValue = value.Trim();

            return cleanValue == trueValue;
        }

        public static IFlatObject ToFlatObject<T>(this string value) where T : IFlatObject
        {
            return new LineBreaker<T>(value).Object;
        }
    }
}
