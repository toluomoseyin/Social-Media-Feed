using SocialMediaApp.Application.DTOs;

namespace SocialMediaApp.Application.Interfaces.Services
{
    public interface ILikeService
    {
        Task<(BaseResponse<string>, int)> AddCommentLike(CommentLikeDTO commentLike, int userId);
        Task<(BaseResponse<string>, int)> AddPostLike(LikePostDTO postLike, int userId);
    }
}
