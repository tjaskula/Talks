
namespace Alt.Net.Step0
{
    public class CurrentUser : ICurrentUser
    {
        public CurrentUser( string userId, string userName, AuthenticationLevel authLevel )
        {
            UserId = userId;
            UserName = userName;
            AuthenticationLevel = authLevel;
        }

        public string UserId { get; private set; }

        public string UserName { get; private set; }

        public AuthenticationLevel AuthenticationLevel { get; private set; }
    }
}
