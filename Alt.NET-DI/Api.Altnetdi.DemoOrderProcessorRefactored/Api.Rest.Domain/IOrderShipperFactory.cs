using Api.Rest.Domain.Processors;

namespace Api.Rest.Domain
{
	public interface IOrderShipperFactory
	{
		IOrderShipper GetOrderShipper();
		void Release(IOrderShipper orderShipper);
	}
}