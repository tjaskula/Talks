using System;
using Api.Rest.Infrastructure.Messages;

namespace Api.Rest.Infrastructure.MessageHandlers
{
	public class UpdatePreferedCustomerStatusHandler : IMessageHandler<OrderValidatedForCustomerMessage>, IDisposable
	{
		public Type MessageType
		{
			get { return typeof(OrderValidatedForCustomerMessage); }
		}

		public Guid InstanceId { get; private set; }

		public UpdatePreferedCustomerStatusHandler()
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