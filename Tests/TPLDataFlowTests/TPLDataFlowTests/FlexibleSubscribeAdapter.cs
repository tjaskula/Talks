﻿using System;
using System.Collections.Generic;
using System.Reflection;

namespace TPLDataFlowTests
{
    public class FlexibleSubscribeAdapter
    {
        private readonly MessageEndpointsBuilder _builder = new MessageEndpointsBuilder();

        public FlexibleSubscribeAdapter RegisterMethods(string methodName)
        {
            AddToScanners(new MethodScanner(methodName));
            return this;
        }

        public FlexibleSubscribeAdapter RegisterMethods(Func<MethodInfo, bool> methodSelector)
        {
            AddToScanners(new MethodScanner(methodSelector));
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