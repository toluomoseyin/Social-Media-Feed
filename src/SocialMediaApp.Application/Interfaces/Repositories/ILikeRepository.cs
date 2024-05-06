using SocialMediaApp.Model.Entities;

namespace SocialMediaApp.Application.Interfaces.Repositories
{
    public interface ILikeRepository
    {
        Task<int> Add(CommentLike commentLike);
        Task<CommentLike> GetCommentLikeByUsercomment(int commentId, int userId);
        Task<PostLike> GetByUserPost(int postId, int userId);
        Task<int> Add(PostLike postLike);
    }
}
