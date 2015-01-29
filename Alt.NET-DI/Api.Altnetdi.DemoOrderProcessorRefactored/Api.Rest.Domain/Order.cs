using System;
using System.Collections.Generic;
using Api.Rest.Domain.Processors;

namespace Api.Rest.Domain
{
	public class Order
	{
		public int Id { get; private set; }
		public DateTime Placed { get; private set; }
		public Address BillingAddress { get; private set; }
		public Address ShippingAddress { get; private set; }
		public ShippingMethod ShippingMethod { get; private set; }
		public bool IsComplete { get; private set; }
		public IEnumerable<OrderLine> OrderLines { get; private set; }
		public int CustomerId { get; private set; }

		public Price GetPrice(IRateExchange exchange, IUserContext userContext)
		{
			User currentUser = userContext.GetCurrentUser();
			Currency currency = userContext.GetSelectedCurrency(currentUser);
			int priceInSelectedCurrency = exchange.Convert(GetPrice(), currency);
			var price = new Price { Currency = currency, Value = priceInSelectedCurrency };
			return price;
		}

		private int GetPrice()
		{
			//do work to aggregate prices from line items, and total up order.
			return 1000;
		}
	}
}