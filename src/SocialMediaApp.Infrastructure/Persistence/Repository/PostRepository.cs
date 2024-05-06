using Microsoft.EntityFrameworkCore;
using SocialMediaApp.Application.DTOs;
using SocialMediaApp.Application.Interfaces.Repositories;
using SocialMediaApp.Infrastructure.Persistence.Context;
using SocialMediaApp.Model.Entities;

namespace SocialMediaApp.Infrastructure.Persistence.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly AppDbContext _appDbContext;

        public PostRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<int> Add(Post post)
        {
            await _appDbContext.Posts.AddAsync(post);

            return await _appDbContext.SaveChangesAsync();
        }

        public async Task<FeedResponse> GetPost(int postId)
        {

            var postDB = await (
           from post in _appDbContext.Posts
           join user in _appDbContext.Users on post.UserId equals user.Id
           where post.Id == postId
           select new FeedResponse
           {
               PostId = post.Id,
               Content = post.Content,
               UserId = post.UserId,
               FirstName = user.FirstName,
               LastName = user.LastName,
               Username = user.Username,
               CreatedDate = post.CreatedDate,
               UpdatedDate = post.UpdatedDate

           }).FirstOrDefaultAsync();

            return postDB;
        }

    }
}
