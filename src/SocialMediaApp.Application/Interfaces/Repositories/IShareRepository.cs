using SocialMediaApp.Model.Entities;

namespace SocialMediaApp.Application.Interfaces.Repositories
{
    public interface IShareRepository
    {
        Task<Share> GetByUserShare(int postId, int userId);
        Task<int> Add(Share share);
    }
}
