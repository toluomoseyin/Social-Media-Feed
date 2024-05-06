using SocialMediaApp.Application.DTOs;
using SocialMediaApp.Application.Interfaces.Repositories;
using SocialMediaApp.Application.Interfaces.Services;

namespace SocialMediaApp.Infrastructure.Services
{
    public class LikeService: ILikeService
    {
        private readonly ILikeRepository _likeRepository;

        public LikeService(ILikeRepository likeRepository)
        {
            _likeRepository = likeRepository;
        }


        public async Task<(BaseResponse<string>, int)> AddPostLike(LikePostDTO postLike, int userId)
        {
            var postLikeDb = await _likeRepository.GetByUserPost(postLike.PostId, userId);

            if (postLikeDb is not null)
            {
                return (BaseResponse<string>.Failure("User already likes post"), 400);
            }

            var rowAffected = await _likeRepository.Add(new Model.Entities.PostLike
            {
                PostId = postLike.PostId,
                UserId = userId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            });

            if (rowAffected > 0)
            {
                return (BaseResponse<string>.Success("Successfully liked post"), 200);

            }

            return (BaseResponse<string>.Failure("Unable to like post"), 500);
        }

        public async Task<(BaseResponse<string>, int)> AddCommentLike(CommentLikeDTO commentLike, int userId)
        {
            var commentLikeDb = await _likeRepository.GetCommentLikeByUsercomment(commentLike.CommentId, userId);

            if (commentLikeDb is not null)
            {
                return (BaseResponse<string>.Failure("User already likes comment"), 400);
            }

            var rowAffected = await _likeRepository.Add(new Model.Entities.CommentLike
            {
                CommentId = commentLike.CommentId,
                UserId = userId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            });

            if (rowAffected > 0)
            {
                return (BaseResponse<string>.Success("Successfully liked comment"), 200);

            }

            return (BaseResponse<string>.Failure("Unable to like comment"), 500);
        }
    }
}
