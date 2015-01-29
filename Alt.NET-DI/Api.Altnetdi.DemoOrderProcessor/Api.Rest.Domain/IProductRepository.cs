namespace Api.Rest.Domain
{
	public interface IProductRepository
	{
		Product GetProductById(int id);
	}
}