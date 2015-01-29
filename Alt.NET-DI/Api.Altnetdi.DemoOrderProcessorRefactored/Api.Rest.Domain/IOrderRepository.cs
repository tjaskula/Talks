namespace Api.Rest.Domain
{
	public interface IOrderRepository
	{
		Order GetOrderById(int id);
	}
}