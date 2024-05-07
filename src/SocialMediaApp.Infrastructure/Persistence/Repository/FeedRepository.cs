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

         var feedResponses = await (
                from post in _appDbContext.Posts
                join user in _appDbContext.Users on post.UserId equals user.Id
                join follow in _appDbContext.Follows on post.UserId equals follow.FolloweeUserId into follows
                from follow in follows.DefaultIfEmpty()
                join postLike in _appDbContext.PostLikes on post.Id equals postLike.PostId into postLikes
                where follow.FollowerUserId == userId || post.UserId == userId
                select new FeedResponse
                {
                    PostId = post.Id,
                    Content = post.Content,
                    UserId = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Username = user.Username,
                    CreatedDate = post.CreatedAt,
                    UpdatedDate = post.UpdatedAt
                }
            )
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Distinct()
            .ToListAsync();

            return feedResponses;
        }

        
    }
}
