using System.Collections.Generic;

namespace TPLDataFlowTests
{
    public interface ISubscriptionResolver
    {
        IEnumerable<ISubscription> GetSubscriptionsFor(object message);

        bool Add(ISubscription subscription);
    }
}