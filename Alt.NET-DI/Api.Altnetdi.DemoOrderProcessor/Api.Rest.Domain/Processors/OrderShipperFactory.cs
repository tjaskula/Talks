using System;

namespace Api.Rest.Domain.Processors
{
    public class OrderShipperFactory
    {
        public static Func<IOrderShipper> CreationClosure;
        public IOrderShipper GetDefault()
        {
            return CreationClosure();//executes closure
        }
    }
}