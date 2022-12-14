using Yu_Gi_Oh_website.Models.BaseModels;

namespace Yu_Gi_Oh_website.Models.Forum.Models
{
    public class Post : BaseDeletableModel<int>
    {
        public Post()
        {
            
        }

        public ApplicationUser Author { get; set; } = null!;

        public string AuthorId { get; set; } = null!;                                  

        public PostContent PostContent { get; set; } = null!;
        public int PostContentId { get; set; }      

        public bool IsRemoved { get; set; }

        public ForumThread Thread { get; set; } = null!;

        public int ThreadId { get; set; }

        public ICollection<PostVote> Votes { get; set; } = null!;
    }
}