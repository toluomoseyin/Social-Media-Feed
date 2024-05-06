namespace SocialMediaApp.Model.Entities
{
    public class Follow
    {
        public int Id { get; set; }
        public int FolloweeUserId { get; set; }
        public int FollowerUserId { get; set; }

    }
}
