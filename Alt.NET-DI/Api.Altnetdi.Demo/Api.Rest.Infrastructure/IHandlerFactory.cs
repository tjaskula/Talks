using System.Collections.Generic;

namespace Api.Rest.Infrastructure
{
	public interface IHandlerFactory
	{
		IEnumerable<IMessageHandler> GetHandlersFor<TMessage>() where TMessage : Message;
	}
}