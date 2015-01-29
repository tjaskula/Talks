namespace Api.Rest.Domain
{
    public interface IAccountsReceivable
    {
        void Collect(User user, Price price);
    }
}