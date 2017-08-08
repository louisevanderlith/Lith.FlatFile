using Lith.FlatFile.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lith.FlatFile
{
    public class Transformer : ITransformer
    {
        private IDictionary<Type, Func<object, FlatPropertyAttribute, string>> transformationMap;

        public Transformer()
        {
            transformationMap = BuildTransformationMap();
        }

        public string Transform(object value, FlatPropertyAttribute attributes)
        {
            var result = string.Empty;
            var valueType = value.GetType();
            var valueToUse = value ?? attributes.DefaultValue;
            var trueType = valueType.BaseType == typeof(Enum) ? typeof(Enum) : valueType;

            if (transformationMap.ContainsKey(trueType))
            {
                result = transformationMap[trueType](valueToUse, attributes);
            }
            else
            {
                result = valueToUse.ToString().Pad(attributes.UseNumericPadding, attributes.FieldLength);
            }

            return result;
        }

        private static IDictionary<Type, Func<object, FlatPropertyAttribute, string>> BuildTransformationMap()
        {
            var result = new Dictionary<Type, Func<object, FlatPropertyAttribute, string>>();

            result.Add(typeof(Enum), TransformEnum);
            result.Add(typeof(bool), TransformBool);
            result.Add(typeof(decimal), TransformDecimal);
            result.Add(typeof(DateTime), TransformDateTime);
            result.Add(typeof(int), TransformInt);

            return result;
        }

        private static string TransformEnum(object value, FlatPropertyAttribute attributes)
        {
            var realValue = (Enum)value;

            return realValue.Transform(attributes.FieldLength);
        }

        private static string TransformBool(object value, FlatPropertyAttribute attributes)
        {
            var realValue = (bool)value;
            var options = attributes.BooleanOptions.Split('|');
            var trueValue = options.FirstOrDefault();
            var falseValue = options.LastOrDefault();

            return realValue.Transform(trueValue, falseValue);
        }

        private static string TransformDecimal(object value, FlatPropertyAttribute attributes)
        {
            var realValue = (decimal)value;

            return realValue.Transform(attributes.FieldLength, attributes.DecimalPlaces);
        }

        public static string TransformDateTime(object value, FlatPropertyAttribute attributes)
        {
            var realValue = (DateTime)value;

            return realValue.Transform(attributes.StringFormat);
        }

        public static string TransformInt(object value, FlatPropertyAttribute attributes)
        {
            var realValue = (int)value;

            return realValue.ToString().Pad(attributes.UseNumericPadding, attributes.FieldLength);
        }
    }
}
