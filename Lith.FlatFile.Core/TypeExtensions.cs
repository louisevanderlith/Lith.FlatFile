using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Lith.FlatFile.Core
{
    public static class TypeExtensions
    {
        public static MethodInfo[] GetExtensionMethods(this Type t)
        {
            var assemblyTypes = new List<Type>();

            foreach (var item in AppDomain.CurrentDomain.GetAssemblies())
            {
                assemblyTypes.AddRange(item.GetTypes());
            }

            var result = from type in assemblyTypes
                         where !type.IsGenericType
                         && !type.IsNested
                         from method in type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                         where method.IsDefined(typeof(ExtensionAttribute), false)
                         && method.GetParameters()[0].ParameterType == t
                         select method;

            return result.ToArray();
        }

        public static MethodInfo GetExtensionMethod(this Type t, string name)
        {
            var methods = t.GetExtensionMethods();

            return methods.FirstOrDefault(a => a.Name == name);
        }
    }
}
