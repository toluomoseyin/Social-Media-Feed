using SocialMediaApp.Model.Entities;

namespace SocialMediaApp.Application.Interfaces.Repositories
{
    public interface IFollowRepository
    {
        Task<Follow> GetByUserID(int followeeUserId, int followerUserId);
        Task<int> Add(Follow follower);

        Task<List<Follow>> GetUserFollowers(int userId);
    }
}
