using Yu_Gi_Oh_website.Models.BaseModels;


namespace Yu_Gi_Oh_website.Models.Forum.Models
{
    public class PostVote : BaseDeletableModel<int>
    {
        public bool IsUpvote { get; set; }

        public string UserId { get; set; } = null!;

        public ApplicationUser User { get; set; }

        public int PostId { get; set; }

        public Post Post { get; set; } = null!;
    }
}
