using SocialMediaApp.Model.Entities;

namespace SocialMediaApp.Application.Interfaces.Repositories
{
    public interface ICommentRepository
    {
        Task<CommentComment> GetCommentCommentByUserComment(int commentId, int userId);
        Task<int> Add(CommentComment commentComment);
        Task<Comment> GetByUserComment(int postId, int userId);
        Task<int> Add(Comment comment);
    }
}
