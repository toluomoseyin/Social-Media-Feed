using Microsoft.EntityFrameworkCore;
using SocialMediaApp.Application.Interfaces.Repositories;
using SocialMediaApp.Infrastructure.Persistence.Context;
using SocialMediaApp.Model.Entities;

namespace SocialMediaApp.Infrastructure.Persistence.Repository
{
    public class ShareRepository: IShareRepository
    {
        private readonly AppDbContext _context;

        public ShareRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> Add(Share share)
        {
            await _context.Shares.AddAsync(share);
            return await _context.SaveChangesAsync();
        }

        public async Task<Share> GetByUserShare(int postId, int userId)
        {
            return await _context.Shares.FirstOrDefaultAsync(x => x.PostId == postId && x.UserId == userId);
        }
    }
}
