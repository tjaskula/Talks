
namespace Alt.Net
{
    public enum AuthenticationLevel
    {
        None = 0,
        Authenticated = 1,
        HighlySecure = 2
    }

    public interface ICurrentUser
    {
        string UserId { get; }

        string UserName { get; }

        AuthenticationLevel AuthenticationLevel { get; }
    }

}
