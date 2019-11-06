using Lith.FlatFile.Core.Models;
using System.Linq;

namespace Lith.FlatFile.Core
{
    public static class FlatteningHelper
    {
        public static string Filler(int fillValue)
        {
            return new string(' ', fillValue);
        }

        public static Point GetDatePoint(string format, char identifier)
        {
            var result = new Point();

            if (format.Contains(identifier))
            {
                var start = format.IndexOf(identifier);
                var end = format.LastIndexOf(identifier) + 1;
                var length = end - start;

                result = new Point(start, length);
            }

            return result;
        }

        public static int GetPointValue(Point datePoint, string rawDate)
        {
            var result = 0;
            if (datePoint.Length != 0)
            {
                var value = rawDate.Substring(datePoint.Start, datePoint.Length);

                int.TryParse(value, out result);
            }

            return result;
        }

        public static int GetPointValue(string format, char identifier, string rawDate)
        {
            var point = GetDatePoint(format, identifier);

            return GetPointValue(point, rawDate);
        }

        public static int GetDecimalMultiplier(int zeros)
        {
            var multiplierTemplate = string.Format("1{0}", new string('0', zeros));
            var multiplier = int.Parse(multiplierTemplate);

            return multiplier;
        }
    }
}
