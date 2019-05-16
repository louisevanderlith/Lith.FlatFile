using Lith.FlatFile.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lith.FlatFile
{
    public class Parser
    {
        private IDictionary<Type, Func<string, FlatPropertyAttribute, object>> _parseMap;
        private int _lastIndex;
        private Type _targetType;

        public Parser()
        {
            _parseMap = BuildParseMap();

            _lastIndex = default(int);
        }

        public object Parse(Type targetType, string fullLine, FlatPropertyAttribute attributes)
        {
            _targetType = targetType;

            var result = default(object);
            var propSection = GetPropertyFromLine(fullLine, attributes.FieldLength);
            var trueType = targetType.BaseType == typeof(Enum) ? typeof(Enum) : targetType;

            if (_parseMap.ContainsKey(trueType))
            {
                result = _parseMap[trueType](propSection, attributes);
            }
            else if (typeof(IFlatObject).IsAssignableFrom(trueType))
            {
                result = _parseMap[typeof(IFlatObject)](propSection, attributes);
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
                { typeof(long), ParseLong},
                { typeof(string), ParseString},
                { typeof(char), ParseChar},
                { typeof(DateTime), ParseDateTime},
                { typeof(IFlatObject), ParseFlatObject }
            };

            return result;
        }

        private string GetPropertyFromLine(string line, int length)
        {
            var result = line.Substring(_lastIndex, length);
            _lastIndex += length;

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

        private object ParseLong(string value, FlatPropertyAttribute attributes)
        {
            var result = default(long);
            long.TryParse(value, out result);

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

        private object ParseFlatObject(string value, FlatPropertyAttribute attributes)
        {
            var type = typeof(LineBreaker<>).MakeGenericType(_targetType);
            var breakerInst = Activator.CreateInstance(type, value);

            var objProp = type.GetProperty("Object");
            
            return  objProp.GetValue(breakerInst);
        }
    }
}
