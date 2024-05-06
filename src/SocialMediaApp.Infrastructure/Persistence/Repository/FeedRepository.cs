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
                                  join follower in _appDbContext.Follows on post.UserId equals follower.FolloweeUserId
                                  where follower.FollowerUserId == userId || post.UserId == userId
                                  select new
                                  {
                                      Post = post,
                                      User = user,
                                  }
                              )
                              .OrderByDescending(feed => _appDbContext.PostLikes.Count(pl => pl.PostId == feed.Post.Id))
                              .Select(feed => new FeedResponse
                              {
                                  PostId = feed.Post.Id,
                                  Content = feed.Post.Content,
                                  UserId = feed.Post.UserId,
                                  FirstName = feed.User.FirstName,
                                  LastName = feed.User.LastName,
                                  Username = feed.User.Username,
                                  CreatedDate = feed.Post.CreatedAt,
                                  UpdatedDate = feed.Post.UpdatedAt,
                              })
                              .Skip((pageNumber - 1) * pageSize)
                              .Take(pageSize)
                              .ToListAsync();

            return Feeds;

        }
    }
}
