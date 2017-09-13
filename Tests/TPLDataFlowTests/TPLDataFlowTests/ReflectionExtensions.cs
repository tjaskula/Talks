using System;
using System.Linq;
using System.Reflection;

namespace TPLDataFlowTests
{
    public static class ReflectionExtensions
    {
        public static bool CanBeCastTo<T>(this Type type)
        {
            return typeof(T).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo());
        }

        public static bool ImplementsInterface<T>(this Type type) where T : class
        {
            return type.GetTypeInfo().ImplementedInterfaces.Any(t => t == typeof(T));
        }
    }
}