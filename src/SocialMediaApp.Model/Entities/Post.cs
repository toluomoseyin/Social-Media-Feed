using System.ComponentModel.DataAnnotations;

namespace SocialMediaApp.Model.Entities
{
    public class Post: BaseEntity
    {
        [StringLength(140)]
        [Required]
        public string Content { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public List<Comment> Comments { get; set; }
        public List<PostLike> PostLikes { get; set; }
        public List<Share> Shares { get; set; } 

        public Post()
        {
            Comments = [];
            PostLikes = [];
            Shares = [];
        }
    }
}
