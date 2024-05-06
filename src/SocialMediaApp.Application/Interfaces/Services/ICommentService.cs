using SocialMediaApp.Application.DTOs;

namespace SocialMediaApp.Application.Interfaces.Services
{
    public interface ICommentService
    {
        Task<(BaseResponse<string>, int)> AddComment(CommentDTO commentDTO, int userId);
        Task<(BaseResponse<string>, int)> AddCommentOnComment(CommentOnCommentDTO commentOnComment, int userId);
    }
}
