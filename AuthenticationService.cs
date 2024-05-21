using System;

namespace FullProject
{
    public interface IAuthenticationService
    {
        bool IsAuthenticated { get; }
        string Username { get; }
        string UserRole { get; }
        void SetAuthenticated(bool isAuthenticated, string username, string userRole);
    }

    public class AuthenticationService : IAuthenticationService
    {
        public bool IsAuthenticated { get; private set; }
        public string Username { get; private set; }
        public string UserRole { get; private set; }

        public void SetAuthenticated(bool isAuthenticated, string username, string userRole)
        {
            IsAuthenticated = isAuthenticated;
            Username = username;
            UserRole = userRole;
        }
    }
}
