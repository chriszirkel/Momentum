using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Momentum.Extensions
{
    public static class MoveCorrespondingExtension
    {
        public static void MoveCorresponding<T>(this T source, T target)
        {
            var properties = typeof(T).GetTypeInfo().DeclaredProperties.Where((p) => p.CanRead && p.CanWrite);

            foreach(var property in properties)
            {
                property.SetValue(target, property.GetValue(source));
            }
        }
    }
}
