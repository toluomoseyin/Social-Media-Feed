using SocialMediaApp.Application.DTOs;

namespace SocialMediaApp.Application.Interfaces.Services
{
    public interface IFollowService
    {
        Task<(BaseResponse<string>, int)> AddFollow(FollowDTO followDTO, int userId);
    }
}
