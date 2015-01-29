using System.Collections.Generic;

namespace Api.Rest.Domain
{
	public interface IProductRepository
	{
		Product GetProductById(int id);
		IEnumerable<Product> GetProductsFormCustomer(int customerId);
	}
}