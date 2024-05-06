using System.ComponentModel.DataAnnotations;

namespace SocialMediaApp.Application.DTOs
{
    public class CommentOnCommentDTO
    {
        [StringLength(140)]
        [Required]
        public string Content { get; set; }
        [Required]
        public int CommentId { get; set; }
    }
}
