namespace Api.Rest.Domain.Processors
{
	public interface IOrderCollector
	{
		void Collect(Order order);
	}
}