using System;
using Api.Rest.Domain;
using Api.Rest.Domain.Processors;

namespace Api.Rest.Application.OrderProcessing
{
	public class OrderValidator : IOrderValidator
	{
		public Guid InstanceId { get; private set; }

		public OrderValidator()
		{
			InstanceId = Guid.NewGuid();
		}

		public bool Validate(Order order)
		{
			return false; //hard-coding just for example's sake.
		}
	}
}