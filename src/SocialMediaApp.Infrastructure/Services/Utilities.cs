using SocialMediaApp.Application.Interfaces.Services;
using BCryptNet = BCrypt.Net.BCrypt;

namespace SocialMediaApp.Infrastructure.Services
{
    public class Utilities : IUtilities
    {
        public string HashPassword(string password)
        {
            return BCryptNet.HashPassword(password);
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            return BCryptNet.Verify(password, hashedPassword);
        }
    }
}
