using System;
using Api.Rest.Infrastructure.Messages;

namespace Api.Rest.Infrastructure
{
	public interface IMessageHandler<TMessage> where TMessage : Message
	{
		Type MessageType { get; }
		void HandleMessage(TMessage message);
	}
}