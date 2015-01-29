namespace Api.Rest.Domain.Providers
{
	public interface ICustomerProvider
	{
		Customer GetCurrentCustomer();
	}
}