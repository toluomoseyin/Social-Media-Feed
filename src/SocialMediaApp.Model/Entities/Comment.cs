using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMediaApp.Model.Entities
{
    public class Comment: BaseEntity
    {
        [StringLength(140)]
        [Required]
        public string Content { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }

        [ForeignKey("UserId")]
        public int? UserId { get; set; }
        public User User { get; set; }
        public List<CommentLike> CommentLikes { get; set; }
        public List<CommentComment> CommentComments { get; set; }
        public Comment()
        {
            CommentLikes = [];
            CommentComments = [];
        }
    }
}
