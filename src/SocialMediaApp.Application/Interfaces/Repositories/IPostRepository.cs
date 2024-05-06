using SocialMediaApp.Application.DTOs;
using SocialMediaApp.Model.Entities;

namespace SocialMediaApp.Application.Interfaces.Repositories
{
    public interface IPostRepository
    {
        Task<FeedResponse> GetPost(int postId);

        Task<int> Add(Post post);
    }
}
