using Yu_Gi_Oh_website.Models.BaseModels;

namespace Yu_Gi_Oh_website.Models.Forum.Models
{
    public class Post : BaseDeletableModel<int>
    {
        public Post()
        {
            
        }

        public ApplicationUser Author { get; set; } = null!;

        public string AuthorId { get; set; }

        public Post ParentPost { get; set; } = null!;
        public int ParentPostId { get; set; }
                                    

        public PostContent PostContent { get; set; } = null!;
        public int PostContentId { get; set; }

        public int Likes { get; set; }
        public int Dislikes { get; set; }

        public bool? Status { get; set; }

        public ForumThread Thread { get; set; } = null!;

        public int ThreadId { get; set; }
    }
}