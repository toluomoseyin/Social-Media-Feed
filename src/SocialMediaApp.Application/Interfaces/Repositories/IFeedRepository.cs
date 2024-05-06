using SocialMediaApp.Application.DTOs;

namespace SocialMediaApp.Application.Interfaces.Repositories
{
    public interface IFeedRepository
    {
        Task<List<FeedResponse>> GetUserFeed(int userId, int pageNumber, int pageSize);
    }
}
