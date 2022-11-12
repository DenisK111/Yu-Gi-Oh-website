namespace Yu_Gi_Oh_website.Web.Areas.Forum.Models
{
    public class ForumThreadDisplayViewModel
    {
        public int Id { get; set; }
        public string Subject { get; set; } = null!;

        public int PostsCount { get; set; }

        public string Author { get; set; } = null!;

        public bool Status { get; set; }

        public string CreatedOn { get; set; } = null!;

        public string? ModifiedOn { get; set; }

        public string Slug { get; set; } = null!;

        public int Views { get; set; }

        public string LastModifiedBy { get; set; } = null!;
    }
}
