using System;

namespace TPLDataFlowTests
{
    public class DispatcherSetup
    {
        public DispatcherSetup AddPolicy<T>(Action<T> policy) where T : ISubscribePolicy
        {
            throw new NotImplementedException();
        }
    }
}