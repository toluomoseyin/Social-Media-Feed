using SocialMediaApp.Model.Entities;

namespace SocialMediaApp.Application.DTOs
{
    public class CommentDTO
    {
        public string Content { get; set; }
        public int PostId { get; set; }

    }
}
