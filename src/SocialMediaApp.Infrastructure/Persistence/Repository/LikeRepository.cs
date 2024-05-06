using Microsoft.EntityFrameworkCore;
using SocialMediaApp.Application.Interfaces.Repositories;
using SocialMediaApp.Infrastructure.Persistence.Context;
using SocialMediaApp.Model.Entities;

namespace SocialMediaApp.Infrastructure.Persistence.Repository
{
    public class LikeRepository: ILikeRepository
    {
        private readonly AppDbContext _context;

        public LikeRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<int> Add(PostLike postLike)
        {
            await _context.PostLikes.AddAsync(postLike);
           return await _context.SaveChangesAsync();
        }

        public async Task<PostLike> GetByUserPost(int postId, int userId)
        {
            return await _context.PostLikes.FirstOrDefaultAsync(x => x.PostId == postId && x.UserId == userId);
        }

        public async Task<int> Add(CommentLike commentLike)
        {
            await _context.CommentLikes.AddAsync(commentLike);
            return await _context.SaveChangesAsync();
        }

        public async Task<CommentLike> GetCommentLikeByUsercomment(int commentId, int userId)
        {
            return await _context.CommentLikes.FirstOrDefaultAsync(x => x.CommentId == commentId && x.UserId == userId);
        }

    }
}
