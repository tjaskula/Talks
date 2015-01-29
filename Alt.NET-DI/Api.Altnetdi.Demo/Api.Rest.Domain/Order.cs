using System;
using System.Collections.Generic;

namespace Api.Rest.Domain
{
	public class Order
	{
		public int Id { get; private set; }
		public DateTime Date { get; private set; }
		public Address BillingAddress { get; private set; }
		public Address ShippingAddress { get; private set; }
		public ShippingMethod ShippingMethod { get; private set; }
		public bool IsComplete { get; private set; }
		public IEnumerable<OrderLine> OrderLines { get; private set; }
		public int CustomerId { get; private set; }
	}
}