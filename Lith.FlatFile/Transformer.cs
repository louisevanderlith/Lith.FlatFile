using Lith.FlatFile.Core;
using Lith.FlatFile.Models;
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

        public string Transform(FlatProperty fProp)
        {
            var result = string.Empty;
            var valueToUse = fProp.Value ?? fProp.Attributes.DefaultValue;

            if (valueToUse == null && fProp.Type.IsValueType)
            {
                valueToUse = Activator.CreateInstance(fProp.Type);
            }

            var trueType = fProp.Type.BaseType == typeof(Enum) ? typeof(Enum) : fProp.Type;

            if (transformationMap.ContainsKey(trueType))
            {
                result = transformationMap[trueType](valueToUse, fProp.Attributes);
            }
            else if (typeof(IFlatObject).IsAssignableFrom(trueType))
            {
                result = transformationMap[typeof(IFlatObject)](valueToUse, fProp.Attributes);
            }
            else
            {
                result = valueToUse.ToString().Pad(fProp.Attributes.UseNumericPadding, fProp.Attributes.FieldLength);
            }

            return result;
        }

        private static IDictionary<Type, Func<object, FlatPropertyAttribute, string>> BuildTransformationMap()
        {
            var result = new Dictionary<Type, Func<object, FlatPropertyAttribute, string>>
            {
                { typeof(String), TransformString },
                {typeof(Char), TransformChar },
                { typeof(Enum), TransformEnum },
                { typeof(bool), TransformBool },
                { typeof(decimal), TransformDecimal },
                { typeof(DateTime), TransformDateTime },
                { typeof(int), TransformInt },
                {typeof(IFlatObject), TransformFlatObject }
            };

            return result;
        }

        private static string TransformString(object value, FlatPropertyAttribute attributes)
        {
            var sval = value as string;

            if (sval == null)
            {
                sval = string.Empty;
            }

            if (sval.Length > attributes.FieldLength)
            {
                return sval.Substring(0, attributes.FieldLength);
            }

            return sval.Pad(attributes.UseNumericPadding, attributes.FieldLength);
        }

        private static string TransformChar(object value, FlatPropertyAttribute attributes)
        {
            if (value == null)
            {
                value = ' ';
            }

            return value.ToString().Pad(attributes.UseNumericPadding, attributes.FieldLength);
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

        public static string TransformFlatObject(object value, FlatPropertyAttribute attribute)
        {
            var realValue = (IFlatObject)value;

            return realValue.ToFlatLine();
        }
    }
}
