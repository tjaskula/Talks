namespace Api.Rest.Infrastructure
{
	public interface IMessageHandler
	{
		void HandleMessage(Message message);
	}
}