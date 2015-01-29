using System.Collections.Generic;

namespace Api.Rest.Domain
{
	public interface IOrderService
	{
		Order GetOrderById(int id);
		IEnumerable<Order> GetOrdersToProcess();
	}
}