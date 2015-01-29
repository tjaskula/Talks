namespace Api.Rest.Domain.Processors
{
    public interface IRateExchange
    {
        int Convert(int cents, Currency currency);
    }
}