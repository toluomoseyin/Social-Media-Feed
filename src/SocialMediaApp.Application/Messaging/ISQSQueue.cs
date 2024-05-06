using Amazon.SQS;

namespace SocialMediaApp.Application.Messaging
{
    public interface ISQSQueue
    {
        Task SendMessage(string message);
    }
}
