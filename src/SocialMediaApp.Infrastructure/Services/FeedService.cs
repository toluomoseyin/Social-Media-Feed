using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SocialMediaApp.Application.DTOs;
using SocialMediaApp.Application.Interfaces.Repositories;
using SocialMediaApp.Application.Interfaces.Services;
using SocialMediaApp.Model.Constants;

namespace SocialMediaApp.Infrastructure.Services
{
    public class FeedService : IFeedService
    {
        private readonly IFeedRepository _feedRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRedisRepository _redisRepository;
        private readonly IMemoryCache _memoryCache;
        private readonly IConfiguration _config;

        public FeedService(IFeedRepository feedRepository, IUserRepository userRepository, IRedisRepository redisRepository, IConfiguration config, IMemoryCache memoryCache)
        {
            _feedRepository = feedRepository;
            _userRepository = userRepository;
            _redisRepository = redisRepository;
            _config = config;
            _memoryCache = memoryCache;
        }

        public async Task<(BaseResponse<List<FeedResponse>>, int)> GetUserFeed(int userId, int pageSize, int pageNumber)
        {

            var user = await _userRepository.GetById(userId);

            if (user is null)
            {
                return (BaseResponse<List<FeedResponse>>.Failure("User does not exists"), 400);
            }
            var cachedValue = string.Empty;

            if (_config.GetValue<string>("Environment") == "Development")
                cachedValue = _memoryCache.Get<string>($"{Cache.USER_FEED}{userId}");
            else
                cachedValue = await _redisRepository.GetValueAsync($"{Cache.USER_FEED}{userId}");

            if (!string.IsNullOrEmpty(cachedValue))
            {
                var feed = JsonConvert.DeserializeObject<List<FeedResponse>>(cachedValue);

                var feedToReturn = feed.Skip((pageNumber - 1) * pageSize)
                                           .Take(pageSize)
                                           .ToList();

                return (BaseResponse<List<FeedResponse>>.Success("User feed was successfully fetched", feedToReturn), 200);
            }
            var userFeedDb = await _feedRepository.GetUserFeed(userId, pageNumber, pageSize);

            if(userFeedDb is not null && userFeedDb.Any())
            {
                if (_config.GetValue<string>("Environment") == "Development")
                    _memoryCache.Set<string>($"{Cache.USER_FEED}{userId}", JsonConvert.SerializeObject(userFeedDb));
                else
                    await _redisRepository.SetValueAsync($"{Cache.USER_FEED}{userId}", JsonConvert.SerializeObject(userFeedDb));
            }
            return (BaseResponse<List<FeedResponse>>.Success("User feed was successfully fetched", userFeedDb), 200);

        }
    }
}
