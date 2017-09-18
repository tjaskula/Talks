using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TPLDataFlowTests
{
    internal class MethodMethodInfoScanner : IMethodInfoScanner
    {
        private readonly Func<MethodInfo, bool> _methodSelector;

        public MethodMethodInfoScanner(Func<MethodInfo, bool> methodSelector)
        {
            _methodSelector = methodSelector;
        }

        public MethodMethodInfoScanner(string methodName)
            : this(mi => mi.Name == methodName)
        {
        }

        public IEnumerable<MethodInfo> GetMethodInfos(object targetToAdapt)
        {
            if (targetToAdapt == null) throw new ArgumentNullException("targetToAdapt");
            var candidates = targetToAdapt.GetType().MethodCandidatesForSubscriptionBuilders(_methodSelector).ToList();
            return candidates;
        }
    }
}