namespace Api.Rest.Infrastructure.Messages
{
	public abstract class Message
	{
		public int CutomerId { get; set; }
		public int OrderId { get; set; }
		public string Content { get; set; }
	}
}