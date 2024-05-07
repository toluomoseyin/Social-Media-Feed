namespace SocialMediaApp.Application.Interfaces.Services
{
    public interface IUtilities
    {
        bool VerifyPassword(string password, string hashedPassword);
        string HashPassword(string password);
    }
}
