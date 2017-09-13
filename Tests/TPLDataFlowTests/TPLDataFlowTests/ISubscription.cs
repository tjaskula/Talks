using System;

namespace TPLDataFlowTests
{
    public interface ISubscription
    {
        /// <summary>
        /// Accept the message
        /// </summary>
        void Push(object message);

        /// <summary>
        /// State whether this type of message is handled
        /// </summary>
        bool Handles(Type messageType);
    }
}