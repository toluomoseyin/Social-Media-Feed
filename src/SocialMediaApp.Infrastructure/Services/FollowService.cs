using SocialMediaApp.Application.DTOs;
using SocialMediaApp.Application.Interfaces.Repositories;
using SocialMediaApp.Application.Interfaces.Services;

namespace SocialMediaApp.Infrastructure.Services
{
    public class FollowService : IFollowService
    {
        private readonly IFollowRepository _followRepository;

        public FollowService(IFollowRepository followRepository)
        {
            _followRepository = followRepository;
        }

        public async Task<(BaseResponse<string>, int)> AddFollow(FollowDTO followDTO, int userId)
        {
            var follow = await _followRepository.GetByUserID(followDTO.FolloweeUserId, userId);

            if (follow is not null)
            {
                return (BaseResponse<string>.Failure("User already follows"), 400);
            }

            var rowAffected = await _followRepository.Add(new Model.Entities.Follow
            {
                FolloweeUserId = followDTO.FolloweeUserId,
                FollowerUserId = userId,
            });

            if (rowAffected > 0)
            {
                return (BaseResponse<string>.Success("Successfully followed user"), 200);

            }

            return (BaseResponse<string>.Failure("Unable to follow user"), 500);
        }
    }
}
