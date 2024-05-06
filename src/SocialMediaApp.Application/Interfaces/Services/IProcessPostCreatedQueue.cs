namespace SocialMediaApp.Application.Interfaces.Services
{
    public interface IProcessPostCreatedQueue
    {
        Task Process(string message);
    }
}
