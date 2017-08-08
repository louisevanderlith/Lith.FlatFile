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
            var flatProperties = obj.GetType().GetProperties();

            return from fProp in flatProperties
                   let flatAttr = fProp.GetCustomAttribute<FlatPropertyAttribute>()
                   where flatAttr != null
                   select new FlatProperty
                   {
                       Name = fProp.Name,
                       Value = fProp.GetValue(obj),
                       Attributes = flatAttr
                   };
        }
    }
}
