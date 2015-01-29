using System;
using System.Threading;
using Api.Rest.Domain;
using Api.Rest.Domain.Processors;

namespace Api.Rest.Application.OrderProcessing
{
	public class OrderShipper : IOrderShipper
	{
		public Guid InstanceId { get; private set; }

		public OrderShipper()
		{
			InstanceId = Guid.NewGuid();
			Thread.Sleep(TimeSpan.FromMilliseconds(1500));
		}

		public void Ship(Order order)
		{
			//ship the order
		}
	}
}