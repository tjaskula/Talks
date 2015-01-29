using System;
using Api.Rest.Domain;
using Api.Rest.Domain.Processors;

namespace Api.Rest.Application.OrderProcessing
{
	public class RateExchange : IRateExchange
	{
		public Guid InstanceId { get; private set; }

		public RateExchange()
		{
			InstanceId = Guid.NewGuid();
		}

		public int Convert(int cents, Currency currency)
		{
			return 45;
		}
	}
}