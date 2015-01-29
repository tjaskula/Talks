namespace Api.Rest.Domain.Processors
{
    public interface IOrderShipper
    {
        void Ship(Order order);
    }
}