using Microsoft.EntityFrameworkCore;
using SocialMediaApp.Application.DTOs;
using SocialMediaApp.Application.Interfaces.Repositories;
using SocialMediaApp.Infrastructure.Persistence.Context;

namespace SocialMediaApp.Infrastructure.Persistence.Repository
{
    public class FeedRepository: IFeedRepository
    {
        private readonly AppDbContext _appDbContext;

        public FeedRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<List<FeedResponse>> GetUserFeed(int userId, int pageNumber, int pageSize)
        {
            var Feeds = await (
            from post in _appDbContext.Posts
            join user in _appDbContext.Users on post.UserId equals user.Id
            join follower in _appDbContext.Followers on post.UserId equals follower.FolloweeUserId into followerJoin
            from follower in followerJoin.DefaultIfEmpty()
            where follower.FollowerUserId == userId || post.UserId == userId
            select new FeedResponse
            {
                PostId = post.Id,
                Content = post.Content,
                UserId = post.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                CreatedDate= post.CreatedDate,
                UpdatedDate= post.UpdatedDate,

            }
                        )
                        .Distinct()
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();

            return Feeds;

        }
    }
}
