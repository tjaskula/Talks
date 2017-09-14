using System;

namespace TPLDataFlowTests
{
    public interface ISubscriber
    {
        /// <summary>
        /// Subscribe anything matching the signature
        /// </summary>
        void Subscribe<T>(Action<T> subscription);

        /// <summary>
        /// Subscribe any methods defined on the subscriber. You need to have added and configured the <see cref="FlexibleSubscribePolicy"/>
        /// for this to work
        /// </summary>
        void Subscribe(object subscriber);
    }
}