using Api.Rest.Infrastructure;

namespace Api.Rest.Application
{
	public class MessageDispatcher
	{
		private readonly IHandlerFactory _handlerFactory;

		public MessageDispatcher(IHandlerFactory handlerFactory)
		{
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