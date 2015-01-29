using System.Collections.Generic;
using Api.Rest.Domain;
using Api.Rest.Domain.Providers;

namespace Api.Rest.Data
{
	public class ProductsProvider : IProductsProvider
	{
		private readonly IProductRepository _products;
		private readonly ICustomerProvider _customerProvider;

		public ProductsProvider(IProductRepository products, ICustomerProvider customerProvider)
		{
			_products = products;
			_customerProvider = customerProvider;
		}

		public IEnumerable<Product> GetProductsForCurrentCustomer()
		{
			var customer = _customerProvider.GetCurrentCustomer();
			var productsForCustomer = _products.GetProductsFormCustomer(customer.Id);

			return productsForCustomer;
		}
	}
}