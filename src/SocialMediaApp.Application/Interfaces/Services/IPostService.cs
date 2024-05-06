using SocialMediaApp.Application.DTOs;

namespace SocialMediaApp.Application.Interfaces.Services
{
    public interface IPostService
    {
        Task<(BaseResponse<string>, int)> CreatePost(AddPostDTO addPostDTO, int userId);
    }
}
