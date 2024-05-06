using SocialMediaApp.Application.DTOs;
using SocialMediaApp.Application.Interfaces.Repositories;
using SocialMediaApp.Application.Interfaces.Services;
using SocialMediaApp.Infrastructure.Persistence.Repository;

namespace SocialMediaApp.Infrastructure.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }



        public async Task<(BaseResponse<string>, int)> AddComment(CommentDTO commentDTO, int userId)
        {

            var rowAffected = await _commentRepository.Add(new Model.Entities.Comment
            {
                Content = commentDTO.Content,
                PostId = commentDTO.PostId,
                UserId = userId
            });

            if (rowAffected > 0)
            {
                return (BaseResponse<string>.Success("Successfully commented on post"), 200);

            }

            return (BaseResponse<string>.Failure("Unable to comment on post"), 500);
        }

        public async Task<(BaseResponse<string>, int)> AddCommentOnComment(CommentOnCommentDTO commentOnComment, int userId)
        {

            var rowAffected = await _commentRepository.Add(new Model.Entities.CommentComment
            {
                Content = commentOnComment.Content,
                CommentId = commentOnComment.CommentId,
                UserId = userId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            });

            if (rowAffected > 0)
            {
                return (BaseResponse<string>.Success("Successfully commented on a comment"), 200);

            }

            return (BaseResponse<string>.Failure("Unable to comment on a comment"), 500);
        }
    }
}
