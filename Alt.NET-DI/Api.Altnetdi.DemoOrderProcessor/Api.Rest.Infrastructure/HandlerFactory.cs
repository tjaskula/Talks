using System.Collections.Generic;

namespace Api.Rest.Infrastructure
{
	public class HandlerFactory : IHandlerFactory
	{
		public IEnumerable<IMessageHandler> GetHandlersFor<TMessage>() where TMessage : Message
		{
			throw new System.NotImplementedException();
		}
	}
}