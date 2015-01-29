namespace Api.Rest.Domain
{
	public class OrderLine
	{
		public int Id { get; private set; }
		public decimal ProductCost { get; private set; }
		public string ProductName { get; private set; }
	}
}