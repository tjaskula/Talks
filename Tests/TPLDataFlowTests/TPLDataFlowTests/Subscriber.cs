using System;
using System.Collections.Generic;

namespace TPLDataFlowTests
{
    public class Subscriber : ISubscriber, IDisposable, ISubscriptionResolver
    {
        private readonly CompositeResolver _resolvers = new CompositeResolver();

        public void Subscribe<T>(Action<T> subscription)
        {
            Subscribe((object)subscription);
        }

        public void Subscribe(object subscriber)
        {
            var svc = new FlexibleSubscribeAdapter().RegisterMethods("Handle");
            if (svc == null)
                throw new InvalidOperationException(
                    "No subscription adapter rules were formulated. Apply the FlexibleSubscribeAdapter to state rules how some instance may be wired up into MemBus.");
            svc.WireUpSubscriber(_resolvers, subscriber);
        }

        public IEnumerable<ISubscription> GetSubscriptionsFor(object message)
        {
            var subs = _resolvers.GetSubscriptionsFor(message);
            return subs;
        }

        public bool Add(ISubscription subscription)
        {
            throw new NotImplementedException();
        }

        public void AddResolver(ISubscriptionResolver resolver)
        {
            _resolvers.Add(resolver);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}