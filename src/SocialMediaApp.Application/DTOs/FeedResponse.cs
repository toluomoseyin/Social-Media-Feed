namespace SocialMediaApp.Application.DTOs
{
    public class FeedResponse
    {
        public int PostId { get; set; }
        public string Content { get; set; }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Username { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
