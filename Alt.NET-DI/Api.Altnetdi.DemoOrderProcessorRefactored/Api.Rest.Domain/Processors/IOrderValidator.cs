namespace Api.Rest.Domain.Processors
{
    public interface IOrderValidator
    {
        bool Validate(Order order);
    }
}