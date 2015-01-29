using System.Collections.Generic;

namespace Api.Rest.Domain.Providers
{
	public interface IProductsProvider
	{
		IEnumerable<Product> GetProductsForCurrentCustomer();
	}
}