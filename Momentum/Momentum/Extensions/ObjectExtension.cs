using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Momentum.Extensions
{
    public static class ObjectExtension
    {
        public static TTarget Clone<TTarget>(this object sourceObj)
        {
            var targetObj = Activator.CreateInstance<TTarget>();

            var sourceProperties = sourceObj.GetType().GetTypeInfo().DeclaredProperties.Where((p) => p.CanRead && p.CanWrite);
            var targetProperties = typeof(TTarget).GetTypeInfo().DeclaredProperties.Where((p) => p.CanRead && p.CanWrite);

            foreach (var targetProperty in targetProperties)
            {
                var sourceProperty = sourceProperties.Where(p => p.Name == targetProperty.Name).FirstOrDefault();

                if (sourceProperty != null)
                {
                    targetProperty.SetValue(targetObj, sourceProperty.GetValue(sourceObj));
                }
            }

            return targetObj;
        }
    }
}
