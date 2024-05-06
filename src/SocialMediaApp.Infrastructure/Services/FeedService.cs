using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using SocialMediaApp.Application.DTOs;
using SocialMediaApp.Application.Interfaces.Repositories;
using SocialMediaApp.Application.Interfaces.Services;
using SocialMediaApp.Infrastructure.Persistence.Cache;
using SocialMediaApp.Model.Constants;

namespace SocialMediaApp.Infrastructure.Services
{
    public class FeedService: IFeedService
    {
        private readonly IFeedRepository _feedRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRedisRepository _redisRepository;

        public FeedService(IFeedRepository feedRepository, IUserRepository userRepository, IRedisRepository redisRepository)
        {
            _feedRepository = feedRepository;
            _userRepository = userRepository;
            _redisRepository = redisRepository;
        }

        public async Task<(BaseResponse<List<FeedResponse>>, int)> GetUserFeed(int userId,int pageSize,int pageNumber)
        {

            var user  =await _userRepository.GetById(userId);

            if(user is null)
            {
                return (BaseResponse<List<FeedResponse>>.Failure("User does not exists"), 400);
            }

            var cachedValue = await _redisRepository.GetValueAsync($"{Cache.USER_FEED}{userId}");

            if (!string.IsNullOrEmpty(cachedValue))
            {
                var feed = JsonConvert.DeserializeObject<List<FeedResponse>>(cachedValue);

                var feedToReturn = feed.Skip((pageNumber - 1) * pageSize)
                                           .Take(pageSize)
                                           .ToList();

                return (BaseResponse<List<FeedResponse>>.Success("User feed was successfully fetched", feedToReturn), 200);
            }

            var userFeedDb = await _feedRepository.GetUserFeed(userId,pageNumber, pageSize);

            await _redisRepository.SetValueAsync($"{Cache.USER_FEED}{userId}", JsonConvert.SerializeObject(userFeedDb));

            return (BaseResponse<List<FeedResponse>>.Success("User feed was successfully fetched", userFeedDb), 200);

        }
    }
}
