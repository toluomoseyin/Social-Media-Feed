namespace SocialMediaApp.Model.Entities
{
    public class Follower
    {
        public int Id { get; set; }
        public int FolloweeUserId { get; set; }
        public int FollowerUserId { get; set; }

    }
}
