using Api.Rest.Domain;
using Api.Rest.Domain.Processors;

namespace Api.Rest.Application.OrderProcessing.Shippers
{
    public class LazyOrderShipper : IOrderShipper
    {
        private readonly IOrderShipperFactory _orderShipperFactory;
        private IOrderShipper _orderShipper;

        public LazyOrderShipper(IOrderShipperFactory orderShipperFactory)
        {
            _orderShipperFactory = orderShipperFactory;
        }

        public void Ship(Order order)
        {
            if (_orderShipper == null)
                _orderShipper = _orderShipperFactory.GetOrderShipper();
            _orderShipper.Ship(order);
        }
    }
}