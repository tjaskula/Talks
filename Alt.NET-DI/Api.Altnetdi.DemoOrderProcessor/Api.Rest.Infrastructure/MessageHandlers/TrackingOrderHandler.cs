using System;
using Api.Rest.Infrastructure.Messages;

namespace Api.Rest.Infrastructure.MessageHandlers
{
	public class TrackingOrderHandler : IMessageHandler<ShippedToCustomerMessage>
	{
		public Type MessageType
		{
			get { return typeof(ShippedToCustomerMessage); }
		}

		public Guid InstanceId { get; private set; }

		public TrackingOrderHandler()
		{
			InstanceId = Guid.NewGuid();
		}

		public void HandleMessage(ShippedToCustomerMessage message)
		{
		}
	}
}