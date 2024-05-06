using Microsoft.EntityFrameworkCore;
using SocialMediaApp.Application.Interfaces.Repositories;
using SocialMediaApp.Infrastructure.Persistence.Context;
using SocialMediaApp.Model.Entities;

namespace SocialMediaApp.Infrastructure.Persistence.Repository
{
    public class FollowRepository : IFollowRepository
    {
        private readonly AppDbContext _appDbContext;

        public FollowRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        public async Task<int> Add(Follower follower)
        {
            await _appDbContext.Followers.AddAsync(follower);

            return await _appDbContext.SaveChangesAsync();
        }

        public async Task<Follower> GetByUserID(int followeeUserId,int followerUserId)
        {
            return await _appDbContext.Followers.FirstOrDefaultAsync(x => x.FollowerUserId == followerUserId && x.FolloweeUserId == followeeUserId);

           
        }


        public async Task<List<Follower>> GetUserFollowers(int userId)
        {
            return await _appDbContext.Followers.Where(x => x.FolloweeUserId == userId).ToListAsync();


        }
    }
}
