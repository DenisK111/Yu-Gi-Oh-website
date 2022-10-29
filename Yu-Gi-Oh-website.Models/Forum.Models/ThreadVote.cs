using Yu_Gi_Oh_website.Models.BaseModels;


namespace Yu_Gi_Oh_website.Models.Forum.Models
{
    public class ThreadVote : BaseDeletableModel<int>
    {
        public bool IsUpvote { get; set; }

        public string UserId { get; set; } = null!;

        public ApplicationUser User { get; set; }

        public int ThreadId { get; set; }
        public ForumThread Thread { get; set; } = null!;
    }
}
