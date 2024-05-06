using System.ComponentModel.DataAnnotations;

namespace SocialMediaApp.Model.Entities
{
    public class User: BaseEntity
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Password { get; set; }

        public List<Post> Posts { get; set; }
        public List<PostLike> PostLikes { get; set; }
        public List<Comment> Comments { get; set; }
        public List<CommentLike> CommentLikes { get; set; }
        public List<CommentComment> CommentComments { get; set; }
        public List<Share> Shares { get; set; }
        public User()
        {
            Posts = [];
            PostLikes = [];
            Comments = [];
            CommentLikes = [];
            CommentComments = [];
            Shares = [];
        }
    }
}
