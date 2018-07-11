using Lith.FlatFile.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lith.FlatFile
{
    public class Parser
    {
        private IDictionary<Type, Func<string, FlatPropertyAttribute, object>> parseMap;
        private int lastIndex;

        public Parser()
        {
            parseMap = BuildParseMap();

            lastIndex = default(int);
        }

        public object Parse(Type targetType, string fullLine, FlatPropertyAttribute attributes)
        {
            var result = default(object);
            var propSection = GetPropertyFromLine(fullLine, attributes.FieldLength);
            var trueType = targetType.BaseType == typeof(Enum) ? typeof(Enum) : targetType;

            if (parseMap.ContainsKey(trueType))
            {
                result = parseMap[trueType](propSection, attributes);
            }

            return result;
        }

        private IDictionary<Type, Func<string, FlatPropertyAttribute, object>> BuildParseMap()
        {
            var result = new Dictionary<Type, Func<string, FlatPropertyAttribute, object>>
            {
                { typeof(Enum), ParseEnum },
                { typeof(bool), ParseBool },
                { typeof(decimal), ParseDecimal},
                { typeof(int), ParseInt},
                { typeof(string), ParseString},
                { typeof(char), ParseChar},
                { typeof(DateTime), ParseDateTime}
            };

            return result;
        }

        private string GetPropertyFromLine(string line, int length)
        {
            var result = line.Substring(lastIndex, length);
            lastIndex += length;

            return result;
        }

        private object ParseEnum(string value, FlatPropertyAttribute attributes)
        {
            var enumType = attributes.DefaultValue.GetType();
            var toEnumMethod = value.GetType().GetExtensionMethod("ToEnum");
            var genericMethod = toEnumMethod.MakeGenericMethod(enumType);
            var result = genericMethod.Invoke(null, new[] { value });

            return result;
        }

        private object ParseBool(string value, FlatPropertyAttribute attributes)
        {
            var options = attributes.BooleanOptions.Split('|');
            var trueValue = options.FirstOrDefault();

            return value.ToBool(trueValue);
        }

        private object ParseDecimal(string value, FlatPropertyAttribute attributes)
        {
            return value.ToDecimal(attributes.DecimalPlaces);
        }

        private object ParseInt(string value, FlatPropertyAttribute attributes)
        {
            var result = default(int);
            int.TryParse(value, out result);

            return result;
        }

        private object ParseString(string value, FlatPropertyAttribute attributes)
        {
            return value.Trim();
        }

        private object ParseChar(string value, FlatPropertyAttribute attributes)
        {
            var result = default(object);
            var isOption = attributes.PossibleValues.Contains(value);

            if (isOption)
            {
                result = value.ToCharArray().FirstOrDefault();
            }
            else
            {
                var message = string.Format("{0} isn't a option in {1}", value, attributes.PossibleValues);
                throw new ArgumentOutOfRangeException(message);
            }

            return result;
        }

        private object ParseDateTime(string value, FlatPropertyAttribute attributes)
        {
            return value.ToDateTime(attributes.StringFormat);
        }
    }
}
