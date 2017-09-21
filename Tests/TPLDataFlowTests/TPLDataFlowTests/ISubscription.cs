using System;

namespace TPLDataFlowTests
{
    public interface ISubscription
    {
        void Push(object message);

        bool Handles(Type messageType);
    }
}