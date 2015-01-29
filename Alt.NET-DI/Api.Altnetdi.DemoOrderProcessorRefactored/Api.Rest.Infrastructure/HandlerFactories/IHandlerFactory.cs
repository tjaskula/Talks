using System.Collections.Generic;
using Api.Rest.Infrastructure.Messages;

namespace Api.Rest.Infrastructure.HandlerFactories
{
	public interface IHandlerFactory
	{
		IEnumerable<IMessageHandler<TMessage>> GetHandlersFor<TMessage>() where TMessage : Message;
		void ReleaseHandler<TMessage>(IMessageHandler<TMessage> messageHandler) where TMessage : Message;
	}
}