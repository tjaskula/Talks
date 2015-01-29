using System;
using Api.Rest.Infrastructure.Messages;
using Api.Rest.Infrastructure.NHibernate;

namespace Api.Rest.Infrastructure.MessageHandlers
{
	public class UpdateOrderShipingStatusHandler : IMessageHandler<ShippedToCustomerMessage>, IDisposable
	{
		public Type MessageType
		{
			get { return typeof(ShippedToCustomerMessage); }
		}

		public Guid InstanceId { get; private set; }

		public UpdateOrderShipingStatusHandler()
		{
			InstanceId = Guid.NewGuid();
		}

		public void HandleMessage(ShippedToCustomerMessage message)
		{
		}

		public void Dispose()
		{
			
		}
	}
}