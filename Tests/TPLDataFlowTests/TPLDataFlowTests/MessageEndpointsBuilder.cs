using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace TPLDataFlowTests
{
    internal class MessageEndpointsBuilder
    {
        private readonly List<IMethodInfoScanner> _scanner = new List<IMethodInfoScanner>();

        public void AddScanner(IMethodInfoScanner methodInfoScanner)
        {
            _scanner.Add(methodInfoScanner);
        }

        public IEnumerable<ISubscription> BuildSubscriptions(object targetToAdapt)
        {
            var subscribers = _scanner
                .SelectMany(s => s.GetMethodInfos(targetToAdapt))
                .Distinct();

            foreach (var subscriber in subscribers)
                yield return ConstructSubscription(subscriber, targetToAdapt);
        }

        private static ISubscription ConstructSubscription(MethodInfo info, object target)
        {
            var parameterType = info.GetParameters()[0].ParameterType;
            var fittingDelegateType = typeof(Action<>).MakeGenericType(parameterType);
            var p = Expression.Parameter(parameterType);
            var call = Expression.Call(Expression.Constant(target), info, p);
            var @delegate = Expression.Lambda(fittingDelegateType, call, p);

            var fittingMethodSubscription = typeof(MethodInvocation<>).MakeGenericType(parameterType);
            var sub = Activator.CreateInstance(fittingMethodSubscription, @delegate.Compile());

            return (ISubscription) sub;
        }
    }
}