namespace Api.Rest.Domain.Processors
{
    public interface IUserContext
    {
        Currency GetSelectedCurrency(User currentUser);
        User GetCurrentUser();
    }
}