using System;
using Api.Rest.Infrastructure.Messages;

namespace Api.Rest.Infrastructure.MessageHandlers
{
	public class ApplyDiscountForCustomerHandler : IMessageHandler<OrderValidatedForCustomerMessage>, IDisposable
	{
		public Type MessageType
		{
			get { return typeof(OrderValidatedForCustomerMessage); }
		}

		public Guid InstanceId { get; private set; }

		public ApplyDiscountForCustomerHandler()
		{
			InstanceId = Guid.NewGuid();
		}

		public void HandleMessage(OrderValidatedForCustomerMessage message)
		{
		}

		public void Dispose()
		{
		}
	}
}