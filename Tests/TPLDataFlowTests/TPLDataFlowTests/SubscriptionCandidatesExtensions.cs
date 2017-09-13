using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TPLDataFlowTests
{
    public static class SubscriptionCandidatesExtensions
    {
        public static IEnumerable<MethodInfo> MethodCandidatesForSubscriptionBuilders(this Type reflectedType, Func<MethodInfo, bool> methodSelector)
        {
            //var disposeTokenMethod = reflectedType.ImplementsInterface<IAcceptDisposeToken>()
            //    ? (mi => mi.Name == "Accept" && mi.GetParameters().Length == 1 &&
            //             mi.GetParameters()[0].ParameterType == typeof(IDisposable))
            //    : new Func<MethodInfo, bool>(mi => false);

            var disposeTokenMethod = new Func<MethodInfo, bool>(mi => false);

            return reflectedType.GetRuntimeMethods().ReduceToValidMessageEndpoints(
                mi => mi.DeclaringType == reflectedType && methodSelector(mi),
                disposeTokenMethod);
        }

        public static IEnumerable<MethodInfo> ReduceToValidMessageEndpoints(
            this IEnumerable<MethodInfo> methods,
            Func<MethodInfo, bool> additionalMethodSelector = null,
            Func<MethodInfo, bool> additionalMethodExclusion = null)
        {
            additionalMethodSelector = additionalMethodSelector ?? (info => true);
            additionalMethodExclusion = additionalMethodExclusion ?? (info => false);

            return
                from mi in methods
                where
                    !mi.IsGenericMethod &&
                    !mi.IsStatic &&
                    mi.IsPublic &&
                    //mi.HasOneParameterOrNoneAndReturnsIObservable() &&
                    !additionalMethodExclusion(mi) &&
                    additionalMethodSelector(mi)
                select mi;
        }
    }
}