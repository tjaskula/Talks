using System;
using Api.Rest.Infrastructure.HandlerFactories;
using Api.Rest.Infrastructure.Messages;

namespace Api.Rest.Application
{
	public class MessageDispatcher
	{
		private readonly IHandlerFactory _handlerFactory;

		public Guid InstanceId { get; private set; }

		public MessageDispatcher(IHandlerFactory handlerFactory)
		{
			InstanceId = Guid.NewGuid();
			_handlerFactory = handlerFactory;
		}

		public void Handle<TMessage>(TMessage message) where TMessage : Message
		{
			var handlers = _handlerFactory.GetHandlersFor<TMessage>();
			foreach (var handler in handlers)
			{
				handler.HandleMessage(message);
			}
		}
	}
}