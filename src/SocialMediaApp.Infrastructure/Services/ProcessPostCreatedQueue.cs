using Newtonsoft.Json;
using SocialMediaApp.Application.DTOs;
using SocialMediaApp.Application.Interfaces.Repositories;
using SocialMediaApp.Application.Interfaces.Services;
using SocialMediaApp.Model.Constants;
using SocialMediaApp.Model.Entities;

namespace SocialMediaApp.Infrastructure.Services
{
    public class ProcessPostCreatedQueue: IProcessPostCreatedQueue
    {
        private readonly IFollowRepository _followRepository;
        private readonly IFeedRepository _feedRepository;
        private readonly IRedisRepository _redisRepository;
        private readonly IPostRepository _postRepository;

        public ProcessPostCreatedQueue(IFollowRepository followRepository, IFeedRepository feedRepository, IRedisRepository redisRepository, IPostRepository postRepository)
        {
            _followRepository = followRepository;
            _feedRepository = feedRepository;
            _redisRepository = redisRepository;
            _postRepository = postRepository;
        }


        public async Task Process(string message)
        {
            var post = JsonConvert.DeserializeObject<Post>(message);

            var followers = await _followRepository.GetUserFollowers(post.UserId);

            foreach (var follower in followers)
            {
                var cachedValue = await _redisRepository.GetValueAsync($"{Cache.USER_FEED}{follower.FolloweeUserId}");

                if (!string.IsNullOrEmpty(cachedValue))
                {

                    var postMoreDetails = await _postRepository.GetPost(post.Id);

                    if(post is not null)
                    {
                        var feed = JsonConvert.DeserializeObject<List<FeedResponse>>(cachedValue);

                        feed.Add(postMoreDetails);

                        await _redisRepository.SetValueAsync($"{Cache.USER_FEED}{follower.FolloweeUserId}", JsonConvert.SerializeObject(feed));
                    }

                   

                }
                var userFeedDb = await _feedRepository.GetUserFeed(follower.FolloweeUserId, 1, 2000);

                await _redisRepository.SetValueAsync($"{Cache.USER_FEED}{follower.FolloweeUserId}", JsonConvert.SerializeObject(userFeedDb));
            }


        }



    }
}
