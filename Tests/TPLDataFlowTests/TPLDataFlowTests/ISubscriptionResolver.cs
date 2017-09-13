using System.Collections.Generic;

namespace TPLDataFlowTests
{
    public interface ISubscriptionResolver
    {
        /// <summary>
        /// Returns a list of subscriptions that would handle this message
        /// </summary>
        IEnumerable<ISubscription> GetSubscriptionsFor(object message);

        /// <summary>
        /// Adds a subscription. the return value signals whether the resolver did in fact accept the subscription
        /// </summary>
        bool Add(ISubscription subscription);
    }
}