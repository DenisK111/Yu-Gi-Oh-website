using Ganss.Xss;

namespace Yu_Gi_Oh_website.Services.Forum.Models
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Author { get; set; } = null!;

        public bool IsVoted { get; set; }
        public bool IsUpvote { get; set; }

        public string AuthorCreatedOn { get; set; } = null!;
        public int AuthorPostsCount { get; set; }
              
        public string PostContent { get; set; } = null!;

        public string SanitizedContent => new HtmlSanitizer().Sanitize(this.PostContent);

        public int Likes { get; set; }
        public int Dislikes { get; set; }

        public bool? Status { get; set; }

        public string CreatedOn { get; set; } = null!;

        public string ProfilePic { get; set; } = null!;


    }
}
