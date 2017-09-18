using System;
using System.Collections.Generic;
using System.Reflection;

namespace TPLDataFlowTests
{
    public class FlexibleSubscribePolicy
    {
        private readonly MessageEndpointsBuilder _builder = new MessageEndpointsBuilder();

        public FlexibleSubscribePolicy RegisterMethods(string methodName)
        {
            AddToScanners(new MethodMethodInfoScanner(methodName));
            return this;
        }

        public FlexibleSubscribePolicy RegisterMethods(Func<MethodInfo, bool> methodSelector)
        {
            AddToScanners(new MethodMethodInfoScanner(methodSelector));
            return this;
        }

        public void WireUpSubscriber(ISubscriptionResolver subscriptionResolver, object subscriber)
        {
            foreach (var disposable in SubscriptionsFor(subscriber))
                subscriptionResolver.Add(disposable);
        }

        private IEnumerable<ISubscription> SubscriptionsFor(object subscriber)
        {
            return _builder.BuildSubscriptions(subscriber);
        }

        private void AddToScanners(IMethodInfoScanner builder)
        {
            _builder.AddScanner(builder);
        }
    }
}