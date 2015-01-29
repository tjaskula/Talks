using System.Collections.Generic;
using Api.Rest.Domain;
using Api.Rest.Domain.Providers;
using Castle.Windsor;

namespace Api.Rest.Data
{
	public class ProductsProvider : IProductsProvider
	{
		private readonly IWindsorContainer _windsorContainer;

		public ProductsProvider(IWindsorContainer windsorContainer)
		{
			_windsorContainer = windsorContainer;
		}

		public IEnumerable<Product> GetProductsForCurrentCustomer()
		{
			var products = _windsorContainer.Resolve<IProductRepository>();
			var customerProvider = _windsorContainer.Resolve<ICustomerProvider>();

			var customer = customerProvider.GetCurrentCustomer();
			var productsForCustomer = products.GetProductsFormCustomer(customer.Id);

			return productsForCustomer;
		}
	}
}