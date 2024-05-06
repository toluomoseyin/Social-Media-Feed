using SocialMediaApp.Application.DTOs;

namespace SocialMediaApp.Application.Interfaces.Services
{
    public interface IFeedService
    {
        Task<(BaseResponse<List<FeedResponse>>, int)> GetUserFeed(int userId, int pageSize, int pageNumber);
    }
}
