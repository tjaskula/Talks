namespace Api.Services
{
    public interface ICryptographer
    {
        string CreateSalt();
        string GetPasswordHash(string password, string salt);
    }
}