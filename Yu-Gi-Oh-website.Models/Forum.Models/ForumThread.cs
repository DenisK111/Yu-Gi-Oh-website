using System.ComponentModel.DataAnnotations;
using Yu_Gi_Oh_website.Models.BaseModels;

namespace Yu_Gi_Oh_website.Models.Forum.Models
{
    public class ForumThread : BaseDeletableModel<int>
    {
        public ForumThread()
        {
            Posts = new HashSet<Post>();

        }
        [MaxLength(150)]
        public string Subject { get; set; } = null!;

        public ICollection<Post> Posts { get; set; }

        public ApplicationUser Author { get; set; } = null!;

        public string AuthorId { get; set; } = null!;

        public bool Status { get; set; }

        public SubCattegory SubCattegory { get; set; } = null!;
        public int SubCattegoryId { get; set; }
        //TODO: CHECK FOR DEFAULT VALUE EF CORE
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        [MaxLength(60)]
        public string Slug { get; set; } = null!;

        public ICollection<ThreadVote> Votes { get; set; } = null!;



    }
}