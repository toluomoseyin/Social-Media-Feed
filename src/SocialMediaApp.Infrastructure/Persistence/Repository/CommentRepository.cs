using Microsoft.EntityFrameworkCore;
using SocialMediaApp.Application.Interfaces.Repositories;
using SocialMediaApp.Infrastructure.Persistence.Context;
using SocialMediaApp.Model.Entities;

namespace SocialMediaApp.Infrastructure.Persistence.Repository
{
    public class CommentRepository: ICommentRepository
    {
        private readonly AppDbContext _context;

        public CommentRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<int> Add(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            return await _context.SaveChangesAsync();
        }

        public async Task<Comment> GetByUserComment(int postId, int userId)
        {
            return await _context.Comments.FirstOrDefaultAsync(x => x.PostId == postId && x.UserId == userId);
        }

        public async Task<int> Add(CommentComment commentComment)
        {
            await _context.CommentComments.AddAsync(commentComment);
            return await _context.SaveChangesAsync();
        }

        public async Task<CommentComment> GetCommentCommentByUserComment(int commentId, int userId)
        {
            return await _context.CommentComments.FirstOrDefaultAsync(x => x.CommentId == commentId && x.UserId == userId);
        }
    }
}
