using System;

namespace TPLDataFlowTests
{
    public class MethodInvocation<T> : ISubscription
    {
        private readonly Action<T> _action;

        public MethodInvocation(Delegate action) : this((Action<T>)action)
        {
        }

        public MethodInvocation(Action<T> action)
        {
            _action = action;
        }

        public void Push(object message)
        {
            _action((T)message);
        }

        public bool Handles(Type messageType)
        {
            return messageType.CanBeCastTo<T>();
        }
    }
}