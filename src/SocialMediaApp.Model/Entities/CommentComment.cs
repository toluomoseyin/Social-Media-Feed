using System.ComponentModel.DataAnnotations;

namespace SocialMediaApp.Model.Entities
{
    public class CommentComment:BaseEntity
    {
        [StringLength(140)]
        [Required]
        public string Content { get; set; }
        public int CommentId { get; set; }
        public Comment Comment { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
