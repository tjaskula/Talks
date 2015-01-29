namespace Api.Rest.Domain
{
	public interface ICustomerRepository
	{
		Customer GetCustomerById(int id);
	}
}