using Microsoft.EntityFrameworkCore;
using SocialMediaApp.Model.Entities;

namespace SocialMediaApp.Infrastructure.Persistence.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
       : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Follow> Follows { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Share> Shares { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<PostLike> PostLikes { get; set; }
        public DbSet<CommentComment> CommentComments { get; set; }
        public DbSet<CommentLike> CommentLikes { get; set; }
    }
}
