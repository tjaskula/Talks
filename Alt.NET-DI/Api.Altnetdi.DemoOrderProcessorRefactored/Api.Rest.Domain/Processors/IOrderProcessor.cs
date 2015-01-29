namespace Api.Rest.Domain.Processors
{
	public interface IOrderProcessor
	{
		SuccessResult Process(Order order);
	}
}