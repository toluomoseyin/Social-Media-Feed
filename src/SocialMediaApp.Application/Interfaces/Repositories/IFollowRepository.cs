using SocialMediaApp.Model.Entities;

namespace SocialMediaApp.Application.Interfaces.Repositories
{
    public interface IFollowRepository
    {
        Task<Follower> GetByUserID(int followeeUserId, int followerUserId);
        Task<int> Add(Follower follower);

        Task<List<Follower>> GetUserFollowers(int userId);
    }
}
