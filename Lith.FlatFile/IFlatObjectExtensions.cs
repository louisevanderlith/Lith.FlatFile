using Lith.FlatFile.Models;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Lith.FlatFile
{
    public static class IFlatObjectExtensions
    {
        public static IEnumerable<FlatProperty> GetFlatProperties(this IFlatObject obj)
        {
            var properties = obj.GetType().GetProperties();

            return from prop in properties
                   let flatAttr = prop.GetCustomAttribute<FlatPropertyAttribute>()
                   where flatAttr != null
                   select new FlatProperty
                   {
                       Name = prop.Name,
                       Value = prop.GetValue(obj),
                       Attributes = flatAttr
                   };
        }

        public static IEnumerable<FlatChild> GetFlatChildren(this IFlatObject obj)
        {
            var properties = obj.GetType().GetProperties();
            var listType = typeof(List<IFlatObject>);

            return from prop in properties
                   where prop.PropertyType == listType
                   select new FlatChild
                   {
                       ParentID = obj.ID,
                       Type = prop.PropertyType.GenericTypeArguments.FirstOrDefault(),
                       Value = (IFlatObject)prop.GetValue(obj)
                   };
        }

        public static string ToFlatLine(this IFlatObject obj)
        {
            return new LineBuilder(obj).Line;
        }
    }
}
