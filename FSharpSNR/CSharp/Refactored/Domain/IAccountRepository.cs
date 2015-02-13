namespace Domain
{
    public interface IAccountRepository
    {
        Account FindByEmail(string email);
        void Save(Account account);
    }
}