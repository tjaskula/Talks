using System;

namespace TPLDataFlowTests
{
    public interface ISubscriber
    {
        void Subscribe<T>(Action<T> subscription);

        void Subscribe(object subscriber);
    }
}